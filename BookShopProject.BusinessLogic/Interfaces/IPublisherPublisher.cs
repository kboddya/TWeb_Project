using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IPublisherPublisher: IPublisherUser
    {
        bool CreateBook(BookDbTable book);
        bool UpdateBook(BookDbTable book);
        bool DeleteBook(int id);

        PublisherDbTable PublisherByEmail(string email);

        BookDbTable BookByIsbn(string isbn);
    }
}