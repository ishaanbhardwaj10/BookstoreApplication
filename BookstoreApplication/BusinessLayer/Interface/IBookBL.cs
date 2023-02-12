using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public bool AddBook(BookModel bookDetails);
        public bool UpdateBook(BookModel bookDetails, int bookId);
        public bool DeleteBook(int bookId);
        public List<GetBookModel> GetAllBooks();
        public GetBookModel GetBookById(int bookId);
    }
}
