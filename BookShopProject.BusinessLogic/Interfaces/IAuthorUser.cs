using System.Collections.Generic;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IAuthorUser
    {
        AuthorDbTable GetAuthorById(int id);
        
        AuthorsList GetAuthors();
        
        BookListDb GetBooksByAuthorId(int id);
        
        List<AuthorDbTable> GetAuthorsByPopularity();
    }
}