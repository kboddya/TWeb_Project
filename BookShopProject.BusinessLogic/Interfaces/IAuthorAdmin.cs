using BookShopProject.Domain.Entities.Author;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IAuthorAdmin
    {
        bool AddAuthor(AuthorDbTable author);
        
        bool UpdateAuthor(AuthorDbTable author);
        
        bool DeleteAuthor(int id);
        
        AuthorDbTable GetAuthorById(int id);
        
        AuthorsList GetAuthors();
    }
}