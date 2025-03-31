using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
{
    class SessionBL : UserApi, ISession
    {
        public UserRegisterResult UserRegister(UDbTable data)
        {
            UserRegisterResult result = new UserRegisterResult();
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == data.Email);
            }

            if (user!=null)
            {
                result.Status = false;
                result.StatusMsg = "User already exists";
                return result;
            }
            
            data.RegisterTime = DateTime.Now;
            data.LastLoginTime = DateTime.Now;
            
            using (var db = new UserContext())
            {
                db.Users.Add(data);
                db.SaveChanges();
            }
            result.Status = true;
            result.StatusMsg = "User registered successfully";
            
            return result;
        }
    }
}
