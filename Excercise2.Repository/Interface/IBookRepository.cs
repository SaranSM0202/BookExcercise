using Excercise2.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2.Repository.Interface
{
    public interface IBookRepository
    {
        Task<List<BookModel>> getBooksByPLFT();
        Task<List<BookModel>> getBooksByPLFTUsingSP();
        Task<String> BulkUploadBooks(string jsonStringModel);
    }
}
