using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class WishlistBL: IWishlistBL
    {
        public IWishlistRL wishlistRL;

        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public bool AddToWishlist(int bookID, int userID)
        {
            try
            {
                return this.wishlistRL.AddToWishlist(bookID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteWishlist(int wishlistId, int userId)
        {
            try
            {
                return this.wishlistRL.DeleteWishlist(wishlistId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetWishlistModel> GetWishlistById(int userId)
        {
            try
            {
                return this.wishlistRL.GetWishlistById(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
