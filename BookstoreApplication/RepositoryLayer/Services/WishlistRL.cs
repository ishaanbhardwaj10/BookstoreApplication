using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class WishlistRL: IWishlistRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;
        List<GetWishlistModel> wishList;

        public WishlistRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool AddToWishlist(int bookID, int userID)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAddToWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@bookId", bookID);
                    sqlCommand.Parameters.AddWithValue("@userId", userID);
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
                finally
                {
                    sqlConnection.Close();
                }
        }

        public bool DeleteWishlist(int wishlistId, int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@wishlistId", wishlistId);
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
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
                finally
                {
                    sqlConnection.Close();
                }
        }

        public List<GetWishlistModel> GetWishlistById(int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spGetWishlistByUserId", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        wishList = new List<GetWishlistModel>();
                        while (dataReader.Read())
                        {
                            wishList.Add(new GetWishlistModel()
                            {
                                WishlistId = (int)dataReader["WishlistID"],
                                BookId = (int)dataReader["BookID"],
                                UserId = (int)dataReader["UserID"]
                            });
                        }
                        return wishList;
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
