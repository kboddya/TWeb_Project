using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic.Core
{
    public class UserApi
    {
        internal UserAuthResult UserRegisterAction(UDbTable data)
        {
            UserAuthResult result = new UserAuthResult();
            if (data.Password.Length < 8)
            {
                result.Status = false;
                result.StatusMsg = "Password must be at least 8 characters long";
                result.StatusKey = "Password";
                return result;
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                result.Status = false;
                result.StatusMsg = "Name cannot be empty";
                result.StatusKey = "Name";
                return result;
            }

            if (!data.Email.Contains("@"))
            {
                result.Status = false;
                result.StatusMsg = "Email is not valid";
                result.StatusKey = "Email";
                return result;
            }

            UDbTable user;

            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == data.Email);
            }

            if (user != null)
            {
                result.Status = false;
                result.StatusMsg = "User already exists";
                result.StatusKey = "Email";
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

        internal UserAuthResult UserLoginAction(UDbTable data)
        {
            UserAuthResult result = new UserAuthResult();
            if (data.Password.Length < 8 || !data.Email.Contains("@"))
            {
                result.Status = false;
                result.StatusMsg = "Email or Password is not valid";
                result.StatusKey = "Name";
                return result;
            }
            
            UDbTable user;

            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == data.Email);
            }

            if (user == null)
            {
                result.Status = false;
                result.StatusMsg = "User not found";
                result.StatusKey = "Name";
                return result;
            }
            
            if (user.Password != data.Password)
            {
                result.Status = false;
                result.StatusMsg = "Email or Password is not valid";
                result.StatusKey = "Name";
                return result;
            }
            
            user.LastLoginTime = DateTime.Now;
            user.LastIp = data.LastIp;

            using (var db = new UserContext())
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
            }
            
            result.Status = true;
            result.StatusMsg = "User logged in successfully";
            result.StatusKey = "Name";
            return result;
        }
    }
}
