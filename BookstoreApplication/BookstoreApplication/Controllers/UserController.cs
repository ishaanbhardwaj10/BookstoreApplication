using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Registration(UserRegistrationModel userDetails)
        {
            var response = this.userBL.Register(userDetails);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "registration successful", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "registration failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLoginModel loginDetails)
        {
            var response = this.userBL.Login(loginDetails);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "login successful", token = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "login failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(int userId, UserUpdateModel userDetails)
        {
            var response = this.userBL.Update(userId, userDetails);
            try
            {
                if(response)
                {
                    return this.Ok(new { success = true, message = "user information updated for userId: "+userId, data = true }); 
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to update information" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Forget")]
        public IActionResult Forget(string emailId)
        {
            var response = this.userBL.Forget(emailId);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "forget password link emailed successfully" , token = response });
                    }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to email forget password link" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Reset")]
        public IActionResult Reset(string email, string password, string confirmPassword)
        {
            var response = this.userBL.Reset(email, password, confirmPassword);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "reset password successfull", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to reset password"});
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
