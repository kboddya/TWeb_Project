using System;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
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