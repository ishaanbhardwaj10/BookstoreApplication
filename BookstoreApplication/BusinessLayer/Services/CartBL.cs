using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartBL: ICartBL
    {
        public ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddToCart(CartModel cartModel, int userID)
        {
            try
            {
                return this.cartRL.AddToCart(cartModel, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCart(int cartId, int userId, int cartQuantity)
        {
            try
            {
                return this.cartRL.UpdateCart(cartId, userId, cartQuantity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCart(int cartId, int userId)
        {
            try
            {
                return this.cartRL.DeleteCart(cartId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetCartModel> GetCartById(int userId)
        {
            try
            {
                return this.cartRL.GetCartById(userId);
            }
            catch(Exception)
            {
                throw;
            }
        }


    }
}
