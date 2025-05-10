using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.DBModel;
using BookShopProject.Domain.Entities.Articles;
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

        internal bool DeleteAuthorAction(int id)
        {
            using (var db = new AuthorContext())
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == id);
                if (author == null) return false;
                var a = author.FirstName + " " + author.LastName;
                var books = BooksListAction(a, BSearchParameter.Author);

                foreach (var book in books?.Books)
                {
                    using (var bookdb = new BookContext())
                    {
                        bookdb.Books.Remove(book);
                        bookdb.SaveChanges();
                    }
                }

                db.Authors.Remove(author);
                db.SaveChanges();
            }

            return true;
        }

        internal int AddAuthorAction(AuthorDbTable author)
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

        internal bool AddGenre(string g)
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

        internal int AddPublisherAction(PublisherDbTable publisher)
        {
            using (var db = new PublisherContext())
            {
                var p = db.Publishers
                    .FirstOrDefault(x => x.Name == publisher.Name);
                if (p != null) return p.Id;

                db.Publishers.Add(publisher);
                db.SaveChanges();
            }

            return publisher.Id;
        }

        internal bool DeletePublisherAction(int id)
        {
            using (var db = new PublisherContext())
            {
                var publisher = db.Publishers.FirstOrDefault(x => x.Id == id);
                if (publisher == null) return false;

                List<BookDbTable> books;
                using (var bookdb = new BookContext())
                {
                    books = bookdb.Books.Where(x => x.PublisherId == id).ToList();
                }
                
                if (books.Count != 0)
                {
                    foreach (var b in books)
                    {
                        DeleteBookAction(b.Id);
                    }
                }

                using (var userdb = new UserContext())
                {
                    var user = userdb.Users.FirstOrDefault(x => x.Email == publisher.Email);
                    if (user != null)
                    {
                        user.Role = URole.user;
                        userdb.Users.AddOrUpdate(user);
                        userdb.SaveChanges();
                    }
                }
                
                db.Publishers.Remove(publisher);
                db.SaveChanges();
            }

            return true;
        }

        internal bool EditPublisherAction(PublisherDbTable publisher)
        {
            using (var db = new PublisherContext())
            {
                var p = db.Publishers.FirstOrDefault(x => x.Id == publisher.Id);
                if (p == null) return false;

                if (p.Email != publisher.Email)
                {
                    using (var dbu = new UserContext())
                    {
                        var user = dbu.Users.FirstOrDefault(x => x.Email == p.Email);
                        if (user != null)
                        {
                            user.Role = URole.user;
                            dbu.Users.AddOrUpdate(user);
                            dbu.SaveChanges();
                        }
                    }
                    
                    using (var dbu = new UserContext())
                    {
                        var user = dbu.Users.FirstOrDefault(x => x.Email == publisher.Email);
                        if (user != null)
                        {
                            user.Role = URole.publisher;
                            dbu.Users.AddOrUpdate(user);
                            dbu.SaveChanges();
                        }
                    }
                }

                db.Publishers.AddOrUpdate(publisher);
                db.SaveChanges();
            }

            return true;
        }

        internal bool DeleteGenre(string g)
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


            book.AuthorId = AddAuthorAction(new AuthorDbTable()
            {
                FirstName = book.AuthorFirstName,
                LastName = book.AuthorLastName,
                LastUpdateTime = DateTime.Now
            });
            
            AddGenre(book.Genre);

            book.PublisherId = AddPublisherAction(new PublisherDbTable()
            {
                Name = book.Publisher
            });

            using (var db = new BookContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
            }


            return true;
        }
        
        internal bool CreateArticleAction(ArticleDbTable article)
        {
            if (ArticleByIdAction(article.Id) != null) return false;

            using (var db = new ArticleContext())
            {
                db.Articles.Add(article);
                db.SaveChanges();
            }

            return true;
        }
        
        internal bool UpdateArticleAction(ArticleDbTable article)
        {
            using (var db = new ArticleContext())
            {
                if (db.Articles.FirstOrDefault(x => x.Id == article.Id) == null) return false;

                db.Articles.AddOrUpdate(article);
                db.SaveChanges();
            }

            return true;
        }

        internal bool DeleteArticleAction(int id)
        {
            using (var db = new ArticleContext())
            {
                var article = db.Articles.FirstOrDefault(x => x.Id == id);
                if (article == null) return false;

                db.Articles.Remove(article);
                db.SaveChanges();
            }

            return true;
        }
    }
}