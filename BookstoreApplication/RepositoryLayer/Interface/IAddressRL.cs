using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public bool Add(AddressModel addressDetails, int userId);
        public bool Update(AddressModel addressDetails, int addressId, int userId);
        public bool Delete(int addressId, int userId);
        public List<GetAddressModel> GetAllAddress(int userId);
    }
}
