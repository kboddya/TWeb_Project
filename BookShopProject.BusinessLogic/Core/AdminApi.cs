using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Genre;
using BookShopProject.Domain.Entities.Publisher;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Domain.Enums.User;

namespace BookShopProject.BusinessLogic.Core
{
    public class AdminApi : BaseApi
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

        private bool DeleteAuthorAction(int id)
        {
            using (var db = new AuthorContext())
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == id);
                var a = author.FirstName + " " + author.LastName;
                if (0 == BooksListAction(a, BSearchParameter.Author).Books.Count)
                {
                    return false;
                }

                db.Authors.Remove(author);
                db.SaveChanges();
            }

            return true;
        }

        private int AddAuthorAction(AuthorDbTable author)
        {
            using (var db = new AuthorContext())
            {
                var a = db.Authors
                    .FirstOrDefault(x => x.FirstName == author.FirstName && x.LastName == author.LastName);
                if (a != null)
                {
                    return a.Id;
                }

                author.BirthDate = new DateTime(1753, 01, 01);
                author.DeathDate = new DateTime(1753, 01, 01);
                author.LastUpdateTime = DateTime.Now;

                db.Authors.Add(author);
                db.SaveChanges();
                return db.Authors.FirstOrDefault(x => x.FirstName == author.FirstName && x.LastName == author.LastName)
                    .Id;
            }
        }

        private bool AddGenre(string g)
        {
            using (var db = new GenreContext())
            {
                if (db.Genres.FirstOrDefault(x => x.Name == g) != null) return false;

                var genre = new GenreDbTable
                {
                    Name = g
                };

                db.Genres.Add(genre);
                db.SaveChanges();
            }

            return true;
        }

        private int AddPublisher(string publisher)
        {
            using (var db = new PublisherContext())
            {
                var p = db.Publishers
                    .FirstOrDefault(x => x.Name == publisher);
                if (p != null) return p.Id;

                p = new PublisherDbTable()
                {
                    Name = publisher
                };
                
                db.Publishers.Add(p);
                db.SaveChanges();
                return db.Publishers.FirstOrDefault(x => x.Name == p.Name).Id;
            }
        }

        internal bool DeletePublisherAction(int id)
        {
            using (var db = new PublisherContext())
            {
                var publisher = db.Publishers.FirstOrDefault(x => x.Id == id);
                if (publisher == null) return false;

                db.Publishers.Remove(publisher);
                db.SaveChanges();
            }

            return true;
        }

        internal bool EditPublisherAction(PublisherDbTable publisher)
        {
            using (var db = new PublisherContext())
            {
                if (db.Publishers.FirstOrDefault(x => x.Id == publisher.Id) == null) return false;


                db.Publishers.AddOrUpdate(publisher);
                db.SaveChanges();
            }

            return true;
        }

        private bool DeleteGenre(string g)
        {
            using (var db = new BookContext())
            {
                if (db.Books.FirstOrDefault((b => b.Genre == g)) != null) return false;
            }

            GenreDbTable genre;
            using (var db = new GenreContext())
            {
                genre = db.Genres.FirstOrDefault(x => x.Name == g);
                if (genre == null) return false;
                db.Genres.Remove(genre);
                db.SaveChanges();
            }

            return true;
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
            BookDbTable book;

            using (var db = new BookContext())
            {
                book = db.Books.FirstOrDefault(x => x.Id == Id);
                if (book == null) return false;

                db.Books.Remove(book);
                db.SaveChanges();
            }

            using (var db = new BookContext())
            {
                if (db.Books.FirstOrDefault(x => x.AuthorId == book.AuthorId) == null)
                    DeleteAuthorAction(book.AuthorId);
                if (db.Books.FirstOrDefault(x => x.Genre == book.Genre) == null) DeleteGenre(book.Genre);
            }


            return true;
        }

        internal bool CreateBookAction(BookDbTable book)
        {
            if (BookByIdAction(book.ISBN) != null) return false;
            book.LastUpdateTime = DateTime.Now;


            var author = new AuthorDbTable
            {
                FirstName = book.AuthorFirstName,
                LastName = book.AuthorLastName,
                LastUpdateTime = DateTime.Now
            };

            book.AuthorId = AddAuthorAction(author);
            AddGenre(book.Genre);
            
            book.PublisherId = AddPublisher(book.Publisher);

            using (var db = new BookContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
            }


            return true;
        }
    }
}