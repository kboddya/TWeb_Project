using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.Book;
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
                author.LastUpdateTime = DateTime.Now;
                
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
        
        internal bool UpdateBookAction(BookDbTable book)
        {
            using (var db = new BookContext())
            {
                if (db.Books.FirstOrDefault(x => x.Id == book.Id) == null) return false;
                
                book.LastUpdateTime = DateTime.Now;
                
                db.Books.AddOrUpdate(book);
                db.SaveChanges();
            }

            return true;
        }
        
        internal bool DeleteBookAction(int Id)
        {
            using (var db = new BookContext())
            {
                var book = db.Books.FirstOrDefault(x => x.Id == Id);
                if (book == null) return false;
                
                db.Books.Remove(book);
                db.SaveChanges();
            }

            return true;
        }
        
        internal bool CreateBookAction(BookDbTable book)
        {
            using (var db = new BookContext())
            {
                if (db.Books.FirstOrDefault(x => x.ISBN == book.ISBN) != null) return false;
                
                db.Books.Add(book);
                db.SaveChanges();
            }

            return true;
        }
        
        internal BookDbTable BookByIdAction(int id)
        {
            BookDbTable b;
            using (var db = new BookContext())
            {
                b = db.Books.FirstOrDefault(x => x.Id == id);
            }

            return b;
        }
        
        internal BookList BooksListAction()
        {
            var b = new BookList();
            using (var db = new BookContext())
            {
                b.Books = db.Books.ToList();
            }

            return b;
        }
    }
}