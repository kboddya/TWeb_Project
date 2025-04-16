using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IBookAdmin
    {
        bool CreateBook(BookDbTable book);
        bool UpdateBook(BookDbTable book);
        bool DeleteBook(int id);
        BookDbTable GetBookById(int id);
        BookList GetBooks();
    } 
}