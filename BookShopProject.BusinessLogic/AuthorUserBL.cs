using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Author;

namespace BookShopProject.BusinessLogic
{
    class AuthorUserBL: UserApi, IAuthorUser
    {
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