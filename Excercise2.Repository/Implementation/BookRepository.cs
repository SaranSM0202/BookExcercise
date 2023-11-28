using Excercise2.Repository;
using Excercise2.Repository.EntityClass;
using Excercise2.Repository.Interface;
using Excercise2.Repository.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDBContext _dbCon;
        private readonly IConfiguration configuration;
        public BookRepository(IConfiguration configuration, BookDBContext dbCon)
        {
            this.configuration = configuration;
            _dbCon = dbCon;
        }
        //PLFT - Punlisher,AuthorLastName,AuthorFirstName,Title
        public async Task<List<BookModel>> getBooksByPLFT()
        {
            List<BookModel> bookModel = new List<BookModel>();
            try
            {
                var books = await _dbCon.Book.ToListAsync();
                bookModel = (from echBok in books
                             orderby echBok.Publisher, echBok.AuthorLastName, echBok.AuthorFirstName, echBok.Title
                             select new BookModel
                             {
                                 Publisher = echBok.Publisher,
                                 Title = echBok.Title,
                                 AuthorFirstName = echBok.AuthorFirstName,
                                 AuthorLastName = echBok.AuthorLastName,
                                 Price = echBok.Price
                             }).ToList();
            }
            catch(Exception ex)
            {
            }
            return bookModel;
        }
        public async Task<List<BookModel>> getBooksByPLFTUsingSP()
        {
            string connectionString = configuration.GetConnectionString("BookConnection");
            List<BookModel> books = new List<BookModel>();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetSortedBooksByPLFT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                BookModel book = new BookModel
                                {
                                    Publisher = reader["Publisher"].ToString(),
                                    AuthorLastName = reader["AuthorLastName"].ToString(),
                                    AuthorFirstName = reader["AuthorFirstName"].ToString(),
                                    Title = reader["Title"].ToString(),
                                    Price = (decimal)reader["Price"]
                                };
                                books.Add(book);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return books;
        }
        public async Task<String> BulkUploadBooks(string jsonStringModel)
        {
            string response = string.Empty;
            try
            {
                // Validate JSON string before deserialization
                if (IsValidJson(jsonStringModel))
                {
                    List<Book> newBooks = JsonConvert.DeserializeObject<List<Book>>(jsonStringModel);
                    if (newBooks.Count > 0)
                    {
                        await _dbCon.Book.AddRangeAsync(newBooks);
                        _dbCon.SaveChanges();
                    }
                    response = "No of Records Inserted is " + newBooks.Count;
                }
                else
                    response = "Invalid Json String";
            }
            catch (Exception ex)
            {
                response = "BulkUpload Failed";
            }
            return response;
        }
        // Function to check if a JSON string is valid
        static bool IsValidJson(string strInput)
        {
            try
            {
                JsonConvert.DeserializeObject<List<Book>>(strInput);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
