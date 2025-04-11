using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Enums.User;

namespace BookShopProject.BusinessLogic.Core
{
    public class AdminApi
    {
        internal bool IsAdmin(URole role)
        {
            return role == URole.admin;
        }
        internal bool UpdateAuthorAction(AuthorDbTable author)
        {
            using (var db = new AuthorContext())
            {
                if (db.Authors.FirstOrDefault(x => x.Id == author.Id) == null)
                {
                    return false;
                }

                db.Authors.AddOrUpdate(author);
                db.SaveChanges();
            }

            return true;
        }

        internal bool DeleteAuthorAction(int id)
        {
            using (var db = new AuthorContext())
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == id);
                if (author == null)
                {
                    return false;
                }

                db.Authors.Remove(author);
                db.SaveChanges();
            }

            return true;
        }

        internal bool AddAuthorAction(AuthorDbTable author)
        {
            using (var db = new AuthorContext())
            {
                if (db.Authors.FirstOrDefault(x => x.FirstName == author.FirstName && x.LastName == author.LastName) !=
                    null) return false;
                db.Authors.Add(author);
                db.SaveChanges();
                return true;
            }
        }
        
        internal AuthorDbTable AuthorByIdAction(int id)
        {
            AuthorDbTable a;
            using (var db = new AuthorContext())
            {
                a = db.Authors.FirstOrDefault(x => x.Id == id);
            }

            return a;
        }

        internal AuthorsList AuthorsListAction()
        {
            var a = new AuthorsList();
            using (var db = new AuthorContext())
            {
                a.Authors = db.Authors.ToList();
            }

            return a;
        }
    }
}