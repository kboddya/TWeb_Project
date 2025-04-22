using System.Collections.Generic;
using BookShopProject.Domain.Entities.Book;
using System.Data.Entity.Migrations;
using System.Linq;
using BookShopProject.Domain.Entities.Genre;
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
            
            g.Sort();

            return g;
        }
    }
}