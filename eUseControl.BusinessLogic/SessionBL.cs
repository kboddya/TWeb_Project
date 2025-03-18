using System;
using BookShopProject.Domain.Entities.User;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;

namespace eUseControl.BusinessLogic
{
    class SessionBL : UserApi, ISession
    {
        public UserLoginResult UserLogin(ULoginData data)
        {
            if (data.Credential == "admin" && data.Password == "password")
            {
                return new UserLoginResult
                {
                    Status = true,
                    StatusMsg = "Login successful"
                };
            }
            else
            {
                return new UserLoginResult
                {
                    Status = false,
                    StatusMsg = "Invalid credentials"
                };
            }
        }
    }
}
