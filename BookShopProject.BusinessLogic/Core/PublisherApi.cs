using System;
using System.Data.Entity.Migrations;
using System.Linq;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic.Core
{
    public class PublisherApi: AdminApi
    {
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

            using (var db = new BookContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
            }


            return true;
        }

        internal PublisherDbTable PublisherByEmailAction(string email)
        {
            PublisherDbTable publisher;
            using (var db = new PublisherContext())
            {
                publisher = db.Publishers.FirstOrDefault(x => x.Email == email);
            }

            return publisher;
        }
    }
}