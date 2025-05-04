using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Book;

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

        public BookListDb GetBooksByAuthorId(int id)
        {
            return BookByAuthorIdAction(id);
        }
    }
}