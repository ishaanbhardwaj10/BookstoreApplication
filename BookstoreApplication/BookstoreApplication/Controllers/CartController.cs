using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddToCart(CartModel cartModel)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.cartBL.AddToCart(cartModel, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new {success = true, message = "added to cart successfully", data = response});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add to cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateCart(int cartId, int cartQuantity)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.cartBL.UpdateCart(cartId, userId, cartQuantity);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "updated cart successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to update cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteCart(int cartId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.cartBL.DeleteCart(cartId, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "deleted cart successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to delete cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult GetCartById()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.cartBL.GetCartById(userId);
            try
            {
                if(response != null)
                {
                    return this.Ok(new { success = true, message = "fetched cart successfully", token = response});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to fetch cart" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
