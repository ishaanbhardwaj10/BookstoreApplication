using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddBook(BookModel bookDetails)
        {
            var response = this.bookBL.AddBook(bookDetails);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "book added successfully", data = response});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add new book" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateBook(BookModel bookDetails, int bookId)
        {
            var response = this.bookBL.UpdateBook(bookDetails, bookId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "book updated successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to update book" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            var response = this.bookBL.DeleteBook(bookId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "book deleted successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to delete book" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var response = this.bookBL.GetAllBooks();
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "all books fetched successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to fetch all books" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int bookId)
        {
            var response = this.bookBL.GetBookById(bookId);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "book fetched successfully by Id", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to fetch book by Id" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
