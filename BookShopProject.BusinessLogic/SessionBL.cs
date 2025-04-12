using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
{
    class SessionBL : UserApi, ISession
    {
        public UserAuthResult UserRegister(UDbTable data)
        {
            return UserRegisterAction(data);
        }

        public UserAuthResult UserLogin(UDbTable data)
        {
            return UserLoginAction(data);
        }

        public HttpCookie GenCookie(string mail)
        {
            return Cookie(mail);
        }

        public UserMinimal GetUserByCookie(string httpCookieValue)
        {
            return UserCookie(httpCookieValue);
        }
    }
}