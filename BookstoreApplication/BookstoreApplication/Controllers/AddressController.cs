using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(AddressModel addressDetails)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.addressBL.Add(addressDetails, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "address added successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(AddressModel addressDetails, int addressId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.addressBL.Update(addressDetails, addressId, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "address updated successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to update address" });
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
        public IActionResult Delete(int addressId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.addressBL.Delete(addressId, userId);
            try
            {
                if (response)
                {
                    return this.Ok(new { success = true, message = "address deleted successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to delete address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAllAddress")]
        public IActionResult GetAllAddress()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.addressBL.GetAllAddress(userId);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "address fetched successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to fetch address" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
