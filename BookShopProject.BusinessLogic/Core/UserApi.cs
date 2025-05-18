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
    public class UserApi : BaseApi
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

            if (session.ExpireTime < DateTime.Now)
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


            var config = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<UDbTable, UserMinimal>(); });
            var mapper = config.CreateMapper();


            return mapper.Map<UserMinimal>(user);
        }

        internal bool AddCartAction(OrderDbTable cart)
        {
            using (var db = new OrderContext())
            {
                if (db.Orders.FirstOrDefault(x => x.Id == cart.Id) != null) return false;
                db.Orders.Add(cart);
                db.SaveChanges();
                return true;
            }
        }

        internal bool DeleteCartAction(OrderDbTable order)
        {
            using (var db = new OrderContext())
            {
                var cart = db.Orders.FirstOrDefault(x => x.Id == order.Id && x.UserId == order.UserId && !x.IsBought);
                if (cart == null) return false;
                db.Orders.Remove(cart);
                db.SaveChanges();
                return true;
            }
        }

        internal decimal CountPriceAction(int userId)
        {
            var price = new decimal();
            using (var db = new OrderContext())
            {
                var cart = db.Orders.Where(x => x.UserId == userId && !x.IsBought).ToList();
                if (cart == null) return -1;
                foreach (var item in cart)
                {
                    price += item.Price;
                }
            }

            return price;
        }

        internal bool BuyCartAction(int userId)
        {
            List<OrderDbTable> cart;
            using (var db = new OrderContext())
            {
                cart = db.Orders.Where(x => x.UserId == userId && !x.IsBought).ToList();
                if (cart.Count == 0) return false;

                foreach (var item in cart)
                {
                    using (var bdb = new BookContext())
                    {
                        var b = bdb.Books.FirstOrDefault(x => x.ISBN == item.ISBN);
                        if (b == null) return false;
                        b.CountOfOrders++;
                        bdb.Books.AddOrUpdate(b);
                        bdb.SaveChanges();
                    }

                    item.IsBought = true;
                    db.Orders.AddOrUpdate(item);
                    db.SaveChanges();
                }
            }

            return true;
        }

        internal OrderDbTable OrderByIdAction(int id)
        {
            OrderDbTable a;
            using (var db = new OrderContext())
            {
                a = db.Orders.FirstOrDefault(x => x.Id == id);
            }

            return a;
        }

        internal OrdersList OrdersListAction(int userId)
        {
            var a = new OrdersList();
            using (var db = new OrderContext())
            {
                a.Orders = db.Orders.Where(x => x.UserId == userId && !x.IsBought).ToList();
            }

            using (var db = new BookContext())
            {
                foreach (var or in a.Orders)
                {
                    var b = db.Books.FirstOrDefault(x=>x.ISBN==or.ISBN);
                    if (b == null || b.CountOfOrders >= b.Count)
                    {
                        using (var odb = new OrderContext())
                        {
                            odb.Orders.Remove(or);
                            odb.SaveChanges();
                        }
                        a.Orders.Remove(or);
                    }
                }
            }

            return a;
        }

        // TODO: Static logic and if available (another UsingDb, Book context, return new type) 
    }
}