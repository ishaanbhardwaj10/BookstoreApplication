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
    public class AdminBL: IAdminBL
    {
        public IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AdminLogin(UserLoginModel loginDeatils)
        {
            try
            {
                return this.adminRL.AdminLogin(loginDeatils);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
