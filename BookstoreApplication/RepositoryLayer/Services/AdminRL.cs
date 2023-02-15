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
    public class AdminRL: IAdminRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;

        public AdminRL(IConfiguration config)
        {
            this.config = config;
        }

        public string AdminLogin(UserLoginModel loginDeatils)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAdminLogin", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    string password = EncryptPassword(loginDeatils.Password);

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@email", loginDeatils.Email);
                    sqlCommand.Parameters.AddWithValue("@password", password);

                    var result = sqlCommand.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT AdminID FROM dbo.Admin WHERE Email='" + result + "'";
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
                        new Claim(ClaimTypes.Role, "Admin"),
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
