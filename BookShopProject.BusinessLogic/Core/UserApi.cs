using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BookShopProject.BusinessLogic.DBModel;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Helpers;

namespace BookShopProject.BusinessLogic.Core
{
    public class UserApi: BaseApi
    {
        internal UserAuthResult UserRegisterAction(UDbTable data)
        {
            var result = new UserAuthResult();
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

            var validate = new EmailAddressAttribute();
            if (!validate.IsValid(data.Email))
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

            data.Password = LoginHelper.HashGen(data.Password);
            
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
            
            var validate = new EmailAddressAttribute();
            
            if (data.Password.Length < 8 || !validate.IsValid(data.Email))
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
            
            if (user.Password != LoginHelper.HashGen(data.Password))
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

        internal HttpCookie Cookie(string mail)
        {
            var httpCookie = new HttpCookie("WNCNN")
            {
                Value = CookieGenerator.Create(mail)
            };

            using (var db = new SessionContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(mail))
                {
                    var current = db.Sessions.FirstOrDefault(s => s.Email == mail);
                    
                    if (current == null)
                    {
                        current = new Session
                        {
                            Email = mail,
                            CookieString = httpCookie.Value,
                            ExpireTime = DateTime.Now.AddDays(1)
                        };
                    }
                    else
                    {
                        current.CookieString = httpCookie.Value;
                        current.ExpireTime = DateTime.Now.AddDays(1);
                    }
                    
                    db.Sessions.AddOrUpdate(current);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid email");
                }
            }
            return httpCookie;
        }

        internal bool SignOutAction(string cookie)
        {
            using (var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie);
                if (session == null) return false;
                db.Sessions.Remove(session);
                db.SaveChanges();
                return true;
            }
        }
        
        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            
            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie);
            }

            
            if (session == null) return null;
            
            if(session.ExpireTime < DateTime.Now)
            {
                SignOutAction(cookie);
                return null;
            }
            
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == session.Email);
            }
            
            if (user == null) return null;

            // TODO: Here must be automapper, but now it in other branch
            var um = new UserMinimal()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
            
            return um;
        }
    }
}
