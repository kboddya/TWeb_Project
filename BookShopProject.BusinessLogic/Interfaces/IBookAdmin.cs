using System.Globalization;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IBookAdmin
    {
        bool CreateBook(BookDbTable book);
        bool UpdateBook(BookDbTable book);
        bool DeleteBook(int id);
        BookDbTable GetBookById(long ISBN);
        BookListDb GetBooks(string parameter = "None", BSearchParameter type = 0);
    } 
}