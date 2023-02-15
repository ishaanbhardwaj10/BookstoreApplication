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
    public class FeedbackBL: IFeedbackBL
    {
        public IFeedbackRL feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public bool Add(FeedbackModel feedbackModel, int userID)
        {
            try
            {
                return this.feedbackRL.Add(feedbackModel, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetFeedbackModel> GetAllFeedbacksByUserId(int userID)
        {
            try
            {
                return this.feedbackRL.GetAllFeedbacksByUserId(userID);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
