using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public bool Add(FeedbackModel feedbackModel, int userID);
        public List<GetFeedbackModel> GetAllFeedbacksByUserId(int userID);
    }
}
