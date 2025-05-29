using System.Collections.Generic;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Genre;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IBookUser
    {
        BookDbTable GetBookByISBN(long ISBN);
        
        BookListDb GetBooks(string parameter = "None", BSearchParameter type = BSearchParameter.All);
        
        List<GenreDbTable> GetGenres();
        
        List<ReviewDbTable> GetReviews(long ISBN);
        
        bool AddReview(ReviewDbTable review);

        List<GenreDbTable> GetGenresByPopularity();

    }
}