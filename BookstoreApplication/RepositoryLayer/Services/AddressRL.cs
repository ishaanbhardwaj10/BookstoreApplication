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
    public class AddressRL: IAddressRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;
        List<GetAddressModel> addressList;

        public AddressRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool Add(AddressModel addressDetails, int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAddAddress", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@address", addressDetails.Address);
                    sqlCommand.Parameters.AddWithValue("@city", addressDetails.City);
                    sqlCommand.Parameters.AddWithValue("@state", addressDetails.State);
                    sqlCommand.Parameters.AddWithValue("@typeID", addressDetails.TypeId);
                    sqlCommand.Parameters.AddWithValue("@userID", userId);
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


        public bool Update(AddressModel addressDetails, int addressId, int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUpdateAddress", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@addressID", addressId);
                    sqlCommand.Parameters.AddWithValue("@address", addressDetails.Address);
                    sqlCommand.Parameters.AddWithValue("@city", addressDetails.City);
                    sqlCommand.Parameters.AddWithValue("@state", addressDetails.State);
                    sqlCommand.Parameters.AddWithValue("@typeID", addressDetails.TypeId);
                    sqlCommand.Parameters.AddWithValue("@userID", userId);
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


        public bool Delete(int addressId, int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteAddress", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@addressID", addressId);
                    sqlCommand.Parameters.AddWithValue("@userID", userId);
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

        public List<GetAddressModel> GetAllAddress(int userId)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spGetAllAddress", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@userID", userId);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        addressList = new List<GetAddressModel>();
                        while(dataReader.Read())
                        {
                            addressList.Add(new GetAddressModel()
                            {
                                AddressId = (int)dataReader["AddressID"],
                                Address = dataReader["Address"].ToString(),
                                City = dataReader["City"].ToString(),
                                State = dataReader["State"].ToString(),
                                TypeId = (int)dataReader["TypeID"],
                                UserId = (int)dataReader["UserID"]
                            });
                        }
                        return addressList;
                    }
                    return null;
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


    }
}
