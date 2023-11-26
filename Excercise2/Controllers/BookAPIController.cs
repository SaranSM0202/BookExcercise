using Excercise2.Repository.Interface;
using Excercise2.Repository.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excercise2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookAPIController : ControllerBase
    {
        private IBookRepository _bookRepository;
        private readonly ILogger<BookAPIController> _logger;

        public BookAPIController(ILogger<BookAPIController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }
        [Route("GetBooksByPLFTUsingEntity")]
        [HttpGet]
        public async Task<List<BookModel>> BooksByPLFT()
        {
            return await _bookRepository.getBooksByPLFT();
        }
        [Route("GetBooksByLFTUsingEntity")]
        [HttpGet]
        public async Task<List<BookModel>> BooksByLFT()
        {
            var bookList = await _bookRepository.getBooksByPLFT();
            return (from echBok in bookList
                        orderby echBok.AuthorLastName, echBok.AuthorFirstName, echBok.Title
                        select new BookModel
                        {
                            Publisher = echBok.Publisher,
                            Title = echBok.Title,
                            AuthorFirstName = echBok.AuthorFirstName,
                            AuthorLastName = echBok.AuthorLastName,
                            Price = echBok.Price
                        }).ToList();
        }
        [Route("GetBooksByPLFTUsingSP")]
        [HttpGet]
        public async Task<List<BookModel>> BooksByPLFTUsinfSP()
        {
            return await _bookRepository.getBooksByPLFTUsingSP();
        }
        [Route("GetBooksByLFTUsingSP")]
        [HttpGet]
        public async Task<List<BookModel>> BooksByLFTUsingSP()
        {
            var bookList = await _bookRepository.getBooksByPLFTUsingSP();
            return (from echBok in bookList
                    orderby echBok.AuthorLastName, echBok.AuthorFirstName, echBok.Title
                    select new BookModel
                    {
                        Publisher = echBok.Publisher,
                        Title = echBok.Title,
                        AuthorFirstName = echBok.AuthorFirstName,
                        AuthorLastName = echBok.AuthorLastName,
                        Price = echBok.Price
                    }).ToList();
        }
        [Route("GetTotalPriceOfAllBooks")]
        [HttpGet]
        public async Task<dynamic> TotalPriceOfAllBooks()
        {
            var book = await _bookRepository.getBooksByPLFT();
            var sum = book.Sum(echBok => echBok.Price);
            return "Total Price of All Books is " + sum;
        }
        [Route("BulkUploadToBooks")]
        [HttpPost]
        public async Task<string> BulkUpload(string jsonStringModel)
        {
            string jsonString = "[{\"Publisher\":\"Publisher Z\",\"Title\":\"Book C\",\"AuthorLastName\":\"Johnson\",\"AuthorFirstName\":\"Robert\",\"Price\":15.75},{\"Publisher\":\"Publisher D\",\"Title\":\"Book D\",\"AuthorLastName\":\"Williams\",\"AuthorFirstName\":\"Susan\",\"Price\":12.99}]";
            return await _bookRepository.BulkUploadBooks(jsonString);
        }
    }
}
