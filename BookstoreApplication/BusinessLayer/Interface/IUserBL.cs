using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public bool Register(UserRegistrationModel userDetails);
        public string Login(UserLoginModel loginDeatils);
        public bool Update(int userID, UserUpdateModel userDetails);
        public string Forget(string email);
        public bool Reset(string email, string password, string confirmPassword);
    }
}
