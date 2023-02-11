using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL: IUserRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;

        public UserRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool Register(UserRegistrationModel userDetails)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    var password = EncryptPassword(userDetails.Password);
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAddUser", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@name",userDetails.Name);
                    sqlCommand.Parameters.AddWithValue("@email",userDetails.Email);
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    sqlCommand.Parameters.AddWithValue("@phone",userDetails.Phone);

                    int result = sqlCommand.ExecuteNonQuery();
                    if(result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception)
                {
                    throw;
                }
            
        }

        public string Login(UserLoginModel loginDeatils)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUserLogin", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    string password = EncryptPassword(loginDeatils.Password);

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@email", loginDeatils.Email);
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    
                    var result = sqlCommand.ExecuteScalar();
                    if(result != null)
                    {
                        string query = "SELECT UserID FROM dbo.Users WHERE Email='" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var userId = cmd.ExecuteScalar();
                        string token = this.GenerateToken(result.ToString(), userId.ToString());
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception)
                {
                    throw;
                }
        }

        public bool Update(int userID, UserUpdateModel userDetails)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUpdateUser", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    string password = EncryptPassword(userDetails.Password);
                    
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@userId", userID);
                    sqlCommand.Parameters.AddWithValue("@name", userDetails.Name);
                    sqlCommand.Parameters.AddWithValue("@email", userDetails.Email);
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    sqlCommand.Parameters.AddWithValue("@phone", userDetails.Phone);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception)
                {
                    throw;
                }
        }

        public string Forget(string email)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spForgetUser", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@email", email);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    GetUserModel userModel = new GetUserModel();
                    while(sqlDataReader.Read())
                    {
                        userModel.UserId = (int)sqlDataReader["UserID"];
                        userModel.Name = sqlDataReader["Name"].ToString();
                        userModel.Email = sqlDataReader["Email"].ToString();
                        userModel.Password = sqlDataReader["Password"].ToString();
                        userModel.Phone = sqlDataReader["Phone"].ToString();
                    }


                    if (userModel.Email != null)
                    {
                        string userName = userModel.Name;
                        string userId = userModel.UserId.ToString();
                        string userEmail = userModel.Email;

                        string token = this.GenerateToken(userEmail, userId);

                        MSMQ ms = new MSMQ();
                        ms.SendMessage(token, email, userName);
                        
                        return token.ToString();
                    }
                    else
                    {
                        return null;
                    }

                }
                catch(Exception)
                {
                    throw;
                }
        }

        public bool Reset(string email, string password, string confirmPassword)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    if (password == confirmPassword)
                    {
                        SqlCommand sqlCommand = new SqlCommand("dbo.spResetUser", sqlConnection);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlConnection.Open();

                        sqlCommand.Parameters.AddWithValue("@email", email);
                        var result = sqlCommand.ExecuteScalar();
                        if (result != null)
                        {
                            string userEmail = result.ToString();
                            string newPassword = EncryptPassword(password);
                            string query = "UPDATE dbo.Users SET Password='"+ newPassword + "' WHERE Email='"+ userEmail+"'";
                            SqlCommand cmd = new SqlCommand(query,sqlConnection);
                            int cmdResult = cmd.ExecuteNonQuery();
                            if (cmdResult > 0)
                            {
                                return true;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
        }



        public string GenerateToken(string EmailID, string userID)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config[("Jwt:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, EmailID),
                        new Claim("userID", userID)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }


    }
}
