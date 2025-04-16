using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic
{
    public class BookAdminBL: AdminApi, IBookAdmin
    {
        public bool UpdateBook(BookDbTable book)
        {
            return UpdateBookAction(book);
        }
        
        public bool CreateBook(BookDbTable book)
        {
            return CreateBookAction(book);
        }
        
        public bool DeleteBook(int id)
        {
            return DeleteBookAction(id);
        }
        
        public BookDbTable GetBookById(int id)
        {
            return BookByIdAction(id);
        }
        
        public BookList GetBooks()
        {
            return GetBooks();
        }
    }
}