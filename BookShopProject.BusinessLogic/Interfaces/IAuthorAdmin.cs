using BookShopProject.Domain.Entities.Author;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IAuthorAdmin
    {
        bool UpdateAuthor(AuthorDbTable author);
        
        AuthorDbTable GetAuthorById(int id);
        
        AuthorsList GetAuthors();
        
        bool DeleteAuthor(int id);
    }
}