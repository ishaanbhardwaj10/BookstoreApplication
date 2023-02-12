using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookBL: IBookBL
    {
        public IBookRL bookRL;

        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public bool AddBook(BookModel bookDetails)
        {
            try
            {
                return this.bookRL.AddBook(bookDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBook(BookModel bookDetails, int bookId)
        {
            try
            {
                return this.bookRL.UpdateBook(bookDetails, bookId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetBookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetBookModel GetBookById(int bookId)
        {
            try
            {
                return this.bookRL.GetBookById(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
