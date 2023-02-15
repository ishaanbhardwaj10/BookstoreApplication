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
    public class CartRL: ICartRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;
        List<GetCartModel> cartList;

        public CartRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool AddToCart(CartModel cartModel, int userID)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAddToCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@bookId", cartModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@userId", userID);
                    sqlCommand.Parameters.AddWithValue("@cartQuantity", cartModel.CartQuantity);
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


        public bool UpdateCart(int cartId, int userId, int cartQuantity)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUpdateCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@cartId", cartId);
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@cartQuantity", cartQuantity);
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


        public bool DeleteCart(int cartId, int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@cartId", cartId);
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


        public List<GetCartModel> GetCartById(int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spGetCartByUserId", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        cartList = new List<GetCartModel>();
                        while(dataReader.Read())
                        {
                            cartList.Add(new GetCartModel()
                            {
                                CartId = (int)dataReader["CartID"],
                                BookId = (int)dataReader["BookID"],
                                UserId = (int)dataReader["UserID"],
                                CartQuantity = (int)dataReader["CartQuantity"]
                            });
                        }
                        return cartList;
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
