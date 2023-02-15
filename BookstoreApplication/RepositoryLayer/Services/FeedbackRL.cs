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
    public class FeedbackRL: IFeedbackRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration config;
        List<GetFeedbackModel> feedbackList;

        public FeedbackRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool Add(FeedbackModel feedbackModel, int userID)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spAddFeedback", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@ratings", feedbackModel.Ratings);
                    sqlCommand.Parameters.AddWithValue("@comment", feedbackModel.Comment);
                    sqlCommand.Parameters.AddWithValue("@userID", userID);
                    sqlCommand.Parameters.AddWithValue("@bookID", feedbackModel.BookId);
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


        public List<GetFeedbackModel> GetAllFeedbacksByUserId(int userID)
        {
            sqlConnection = new SqlConnection(this.config.GetConnectionString("BookstoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spGetAllFeedback", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@userId", userID);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        feedbackList = new List<GetFeedbackModel>();
                        while (dataReader.Read())
                        {
                            feedbackList.Add(new GetFeedbackModel()
                            {
                                FeedbackId = (int)dataReader["FeedbackID"],
                                Ratings = (int)dataReader["Ratings"],
                                Comment = dataReader["Comment"].ToString(),
                                UserId = (int)dataReader["UserID"],
                                BookId = (int)dataReader["BookID"]
                            });
                        }
                        return feedbackList;
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
