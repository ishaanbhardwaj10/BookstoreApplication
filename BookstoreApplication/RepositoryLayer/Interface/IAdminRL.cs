using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        public string AdminLogin(UserLoginModel loginDeatils);
    }
}
