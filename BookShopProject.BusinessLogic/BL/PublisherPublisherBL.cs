using System.Collections.Generic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Publisher;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.BusinessLogic
{
    public class PublisherPublisherBL: PublisherApi, IPublisherPublisher
    {
        public PublisherDbTable GetPublisherById(int id)
        {
            return PublisherByIdAction(id);
        }

        public BookListDb GetBooksByPublisherId(int id)
        {
            return BookByPublisherIdAction(id);
        }

        public bool CreateBook(BookDbTable book)
        {
            return CreateBookAction(book);
        }

        public bool UpdateBook(BookDbTable book)
        {
            return UpdateBookAction(book);
        }

        public BookDbTable BookByIsbn(string isbn)
        {
            return BooksListAction(isbn, BSearchParameter.ISBN).Books?[0];
        }

        public bool DeleteBook(int id)
        {
            return DeleteBookAction(id);
        }
        
        public PublisherDbTable PublisherByEmail(string email)
        {
            return PublisherByEmailAction(email);
        }
        
        public List<PublisherDbTable> GetPublishersByPopularity()
        {
            return GetPublishersByPopularityAction();
        }
    }
}