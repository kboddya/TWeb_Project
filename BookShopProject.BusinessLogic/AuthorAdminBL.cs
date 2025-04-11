using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Author;

namespace BookShopProject.BusinessLogic
{
    public class AuthorAdminBL : AdminApi, IAuthorAdmin
    {
        public bool AddAuthor(AuthorDbTable author)
        {
            return AddAuthorAction(author);
        }

        public bool UpdateAuthor(AuthorDbTable author)
        {
            return UpdateAuthorAction(author);
        }

        public bool DeleteAuthor(int id)
        {
            return DeleteAuthorAction(id);
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