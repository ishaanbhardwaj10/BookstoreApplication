using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookRL: IBookRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;
        public List<GetBookModel> bookList;

        public BookRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool AddBook(BookModel bookDetails)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAddBook", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@bookName", bookDetails.BookName);
                    sqlCommand.Parameters.AddWithValue("@authorName", bookDetails.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@ratings", bookDetails.Ratings);
                    sqlCommand.Parameters.AddWithValue("@noOfPeopleRated", bookDetails.NoOfPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@discountedPrice", bookDetails.DiscountedPrice);
                    sqlCommand.Parameters.AddWithValue("@originalPrice", bookDetails.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@bookDetails", bookDetails.BookDetails);
                    sqlCommand.Parameters.AddWithValue("@bookImage", bookDetails.BookImage);
                    sqlCommand.Parameters.AddWithValue("@bookQuantity", bookDetails.BookQuantity);
                    int result = sqlCommand.ExecuteNonQuery();
                    if(result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }


        public bool UpdateBook(BookModel bookDetails, int bookId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUpdateBook", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@bookId", bookId);
                    sqlCommand.Parameters.AddWithValue("@bookName", bookDetails.BookName);
                    sqlCommand.Parameters.AddWithValue("@authorName", bookDetails.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@ratings", bookDetails.Ratings);
                    sqlCommand.Parameters.AddWithValue("@noOfPeopleRated", bookDetails.NoOfPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@discountedPrice", bookDetails.DiscountedPrice);
                    sqlCommand.Parameters.AddWithValue("@originalPrice", bookDetails.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@bookDetails", bookDetails.BookDetails);
                    sqlCommand.Parameters.AddWithValue("@bookImage", bookDetails.BookImage);
                    sqlCommand.Parameters.AddWithValue("@bookQuantity", bookDetails.BookQuantity);
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
        }


        public bool DeleteBook(int bookId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteBook", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@bookId", bookId);
                    int result = sqlCommand.ExecuteNonQuery();
                    if(result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
        }


        public List<GetBookModel> GetAllBooks()
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spGetAllBooks", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    this.bookList = new List<GetBookModel>();
                    //GetBookModel book = new GetBookModel();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            //book.BookId = (int)dataReader["BookID"];
                            //book.BookName = dataReader["BookName"].ToString();
                            //book.AuthorName = dataReader["AuthorName"].ToString();
                            //book.Ratings = (decimal)dataReader["Ratings"];
                            //book.NoOfPeopleRated = (int)dataReader["NoOfPeopleRated"];
                            //book.DiscountedPrice = (int)dataReader["DiscountedPrice"];
                            //book.OriginalPrice = (int)dataReader["OriginalPrice"];
                            //book.BookDetails = dataReader["BookDetails"].ToString();
                            //book.BookImage = dataReader["BookImage"].ToString();
                            //book.BookQuantity = (int)dataReader["BookQuantity"];

                            //bookList.Add(book);

                            //cannot use this approach as it will update the value of all books
                            //in the list to the value last book/record
                            //you need to create a new instance of GetBookModel for each
                            //iteration so that all records can be added seperately

                            bookList.Add(new GetBookModel() {
                                BookId = (int)dataReader["BookID"],
                                BookName = dataReader["BookName"].ToString(),
                                AuthorName = dataReader["AuthorName"].ToString(),
                                Ratings = (decimal)dataReader["Ratings"],
                                NoOfPeopleRated = (int)dataReader["NoOfPeopleRated"],
                                DiscountedPrice = (int)dataReader["DiscountedPrice"],
                                OriginalPrice = (int)dataReader["OriginalPrice"],
                                BookDetails = dataReader["BookDetails"].ToString(),
                                BookImage = dataReader["BookImage"].ToString(),
                                BookQuantity = (int)dataReader["BookQuantity"]
                            });
                        }
                        return bookList;
                    }
                    return null;
                }
                catch(Exception)
                {
                    throw;
                }
        }


        public GetBookModel GetBookById(int bookId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spGetBookById", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@bookId", bookId);
                    GetBookModel book = new GetBookModel();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            book.BookId = (int)dataReader["BookID"];
                            book.BookName = dataReader["BookName"].ToString();
                            book.AuthorName = dataReader["AuthorName"].ToString();
                            book.Ratings = (decimal)dataReader["Ratings"];
                            book.NoOfPeopleRated = (int)dataReader["NoOfPeopleRated"];
                            book.DiscountedPrice = (int)dataReader["DiscountedPrice"];
                            book.OriginalPrice = (int)dataReader["OriginalPrice"];
                            book.BookDetails = dataReader["BookDetails"].ToString();
                            book.BookImage = dataReader["BookImage"].ToString();
                            book.BookQuantity = (int)dataReader["BookQuantity"];
                        }
                        return book;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }
        }

    }
}
