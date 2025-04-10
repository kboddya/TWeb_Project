using BookShopProject.Domain.Entities.Author;
namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IAuthorUser
    {
        AuthorDbTable GetAuthorById(int id);
        
        AuthorsList GetAuthors();
    }
}