using System.Collections.Generic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Genre;
using BookShopProject.Domain.Enums.Book;

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
        
        public BookDbTable GetBookById(long ISBN)
        {
            return BookByIdAction(ISBN);
        }
        
        public BookListDb GetBooks(string parameter = "None", BSearchParameter type = BSearchParameter.All)
        {
            return BooksListAction(parameter, type);
        }
        
        public List<ReviewDbTable> GetReviews(long ISBN)
        {
            return GetReviewsByISBNAction(ISBN);
        }

        public bool DeleteReview(int id)
        {
            return DeleteReviewAction(id);
        }

        public List<BookDbTable> BookStats()
        {
            return BooksListAction(" ", BSearchParameter.Popularity).Books;
        }
        
        public List<GenreDbTable> GenreStats()
        {
            return GetGenresByPopularityAction();
        }
    }
}