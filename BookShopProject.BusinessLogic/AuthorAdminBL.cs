using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Author;

namespace BookShopProject.BusinessLogic
{
    public class AuthorAdminBL : AdminApi, IAuthorAdmin
    {
        public bool UpdateAuthor(AuthorDbTable author)
        {
            return UpdateAuthorAction(author);
        }
        
        public AuthorDbTable GetAuthorById(int id)
        {
            return AuthorByIdAction(id);
        }
        
        public AuthorsList GetAuthors()
        {
            return AuthorsListAction();
        }
    }
}