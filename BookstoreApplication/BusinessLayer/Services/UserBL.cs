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
    public class UserBL: IUserBL
    {
        public IUserRL userRL;

        public UserBL(IUserRL userRL) 
        {
            this.userRL = userRL;
        }

        public bool Register(UserRegistrationModel userDetails)
        {
            try
            {
                return this.userRL.Register(userDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Login(UserLoginModel loginDetails)
        {
            try
            {
                return this.userRL.Login(loginDetails);
            }
            catch(Exception) 
            {
                throw; 
            }
        }

        public bool Update(int userID, UserUpdateModel userDetails)
        {
            try
            {
                return this.userRL.Update(userID, userDetails);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string Forget(string email)
        {
            try
            {
                return this.userRL.Forget(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Reset(string email, string password, string confirmPassword)
        {
            try
            {
                return this.userRL.Reset(email, password, confirmPassword);
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
