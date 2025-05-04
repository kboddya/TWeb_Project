using System.Collections.Generic;
using System.Security.Policy;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IPublisherUser
    {
        PublisherDbTable GetPublisherById(int id);

        BookListDb GetBooksByPublisherId(int id);
    }
}