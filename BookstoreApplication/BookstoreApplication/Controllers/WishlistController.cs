using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;

        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddToWishlist(int bookID)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.wishlistBL.AddToWishlist(bookID, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "added to wishlist successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add to wishlist" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Authorize(Roles = Role.User)]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteWishlist(int wishlistId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.wishlistBL.DeleteWishlist(wishlistId, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "deleted wishlist successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to delete wishlist" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("Get")]
        public IActionResult GetWishlistById()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.wishlistBL.GetWishlistById(userId);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "fetched wishlist successfully", token = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to fetch wishlist" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
