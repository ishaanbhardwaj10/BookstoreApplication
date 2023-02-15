using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult AdminLogin(UserLoginModel loginDeatils)
        {
            var response = this.adminBL.AdminLogin(loginDeatils);
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

    }
}
