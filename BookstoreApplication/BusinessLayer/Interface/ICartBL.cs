using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public bool AddToCart(CartModel cartModel, int userID);
        public bool UpdateCart(int cartId, int userId, int cartQuantity);
        public bool DeleteCart(int cartId, int userId);
        public List<GetCartModel> GetCartById(int userId);
    }
}
