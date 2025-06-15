using System.Collections.Generic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Genre;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.BusinessLogic
{
    public class BookUserBL: UserApi, IBookUser
    {
        public BookDbTable GetBookByISBN(long ISBN)
        {
            return BookByIdAction(ISBN);
        }

        public BookListDb GetBooks(string parameter = "", BSearchParameter type = BSearchParameter.All)
        {
            return BooksListAction(parameter, type);
        }

        public List<GenreDbTable> GetGenres()
        {
            return GenresListAction();
        }
        
        public List<GenreDbTable> GetGenresByPopularity()
        {
            return GetGenresByPopularityAction();
        }

        public List<ReviewDbTable> GetReviews(long ISBN)
        {
            return GetReviewsByISBNAction(ISBN);
        }

        public bool AddReview(ReviewDbTable review)
        {
            return AddReviewAction(review);
        }
    }
}