using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public bool AddToWishlist(int bookID, int userID);
        public bool DeleteWishlist(int wishlistId, int userId);
        public List<GetWishlistModel> GetWishlistById(int userId);
    }
}
