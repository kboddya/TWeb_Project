using System.Collections.Generic;
using BookShopProject.Domain.Entities.Book;
using System.Data.Entity.Migrations;
using System.Linq;
using BookShopProject.BusinessLogic.DBModel;
using BookShopProject.Domain.Entities.Articles;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Genre;
using BookShopProject.Domain.Entities.Publisher;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.BusinessLogic.Core
{
    public class BaseApi
    {
        internal BookDbTable BookByIdAction(long ISBN)
        {
            BookDbTable b;
            using (var db = new BookContext())
            {
                b = db.Books.FirstOrDefault(x => x.ISBN == ISBN);
            }

            return b;
        }

        internal BookListDb BooksListAction(string parameter, BSearchParameter type)
        {
            var b = new BookListDb();
            switch (type)
            {
                case BSearchParameter.Author:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.AuthorFirstName + " " + x.AuthorLastName == parameter).ToList();
                    }

                    break;
                }

                case BSearchParameter.Genre:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.Genre == parameter).ToList();
                    }

                    break;
                }

                case BSearchParameter.Language:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.Language == parameter).ToList();
                    }

                    break;
                }

                case BSearchParameter.Popularity:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.CountOfOrders > 15).ToList();
                    }

                    break;
                }

                case BSearchParameter.Publisher:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.Publisher == parameter).ToList();
                    }

                    break;
                }

                case BSearchParameter.ISBN:
                {
                    b.Books = new List<BookDbTable>();
                    b.Books.Add(BookByIdAction(long.Parse(parameter)));

                    break;
                }

                case BSearchParameter.Title:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.Title.Contains(parameter)).ToList();
                    }

                    break;
                }

                case BSearchParameter.Year:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.PublishDate.Year.ToString() == parameter).ToList();
                    }

                    break;
                }

                case BSearchParameter.Age:
                {
                    var age = (AgeCategories)int.Parse(parameter);
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.age == age).ToList();
                    }
                    
                    break;
                }

                case BSearchParameter.Offers:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.Where(x => x.DiscountedPrice != decimal.MinusOne).ToList();
                    }
                    
                    break;
                }

                case BSearchParameter.All:
                default:
                {
                    using (var db = new BookContext())
                    {
                        b.Books = db.Books.ToList();
                        b.Books.Reverse();
                    }

                    break;
                }
            }

            return b;
        }
        
        internal List<GenreDbTable> GenresListAction()
        {
            var g = new List<GenreDbTable>();
            using (var db = new GenreContext())
            {
                g = db.Genres.ToList();
            }
            

            return g;
        }

        internal BookListDb BookByAuthorIdAction(int id)
        {
            var books = new BookListDb();
            using (var db = new BookContext())
            {
                books.Books = db.Books.Where(x => x.AuthorId == id).ToList();
            }

            return books;
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

        internal PublisherDbTable PublisherByIdAction(int id)
        {
            PublisherDbTable p;
            using (var db = new PublisherContext())
            {
                p = db.Publishers.FirstOrDefault(x => x.Id == id);
            }
            
            return p;
        }
        
        internal PublishersList PublishersListAction()
        {
            var p = new PublishersList();
            using (var db = new PublisherContext())
            {
                p.Publishers = db.Publishers.ToList();
            }

            return p;
        }

        internal BookListDb BookByPublisherIdAction(int id)
        {
            var b =new BookListDb();
            using (var db = new BookContext())
            {
                b.Books = db.Books.Where(x => x.PublisherId == id).ToList();
            }

            return b;
        }

        internal List<ArticleDbTable> ArticlesListAction()
        {
            List<ArticleDbTable> articles;

            using (var db = new ArticleContext())
            {
                articles = db.Articles.ToList();
            }

            articles.Reverse();

            return articles;
        }

        internal ArticleDbTable ArticleByIdAction(int id)
        {
            ArticleDbTable article;

            using (var db = new ArticleContext())
            {
                article = db.Articles.FirstOrDefault(x => x.Id == id);
            }
            
            return article;
        }
        
        internal List<ReviewDbTable> GetReviewsByISBNAction(long isbn)
        {
            using (var db = new ReviewContext())
            {
                return db.Reviews.Where(x => x.ISBN == isbn).ToList();
            }
        }
    }
}