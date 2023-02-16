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
    public class AddressBL : IAddressBL
    {
        public IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool Add(AddressModel addressDetails, int userId)
        {
            try
            {
                return this.addressRL.Add(addressDetails, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(AddressModel addressDetails, int addressId, int userId)
        {
            try
            {
                return this.addressRL.Update(addressDetails, addressId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int addressId, int userId)
        {
            try
            {
                return this.addressRL.Delete(addressId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetAddressModel> GetAllAddress(int userId)
        {
            try
            {
                return this.addressRL.GetAllAddress(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
