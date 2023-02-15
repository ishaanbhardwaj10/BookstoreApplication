using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(FeedbackModel feedbackModel)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.feedbackBL.Add(feedbackModel, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "added feedback successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add feedback" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAllFeedback")]
        public IActionResult GetAllFeedbacksByUserId()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.feedbackBL.GetAllFeedbacksByUserId(userId);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "fetched feedback successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to fetch feedback" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
