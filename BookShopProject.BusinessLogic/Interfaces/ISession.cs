﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserAuthResult UserRegister(UDbTable data);
        UserAuthResult UserLogin(UDbTable data);
        
        HttpCookie GenCookie(string mail);
        
        UserMinimal GetUserByCookie(string httpCookieValue);
        
        bool SignOut(string httpCookieValue);
    }
}