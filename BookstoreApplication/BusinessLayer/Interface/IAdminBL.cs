using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public string AdminLogin(UserLoginModel loginDeatils);
    }
}
