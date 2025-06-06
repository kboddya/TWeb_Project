﻿using System.Collections.Generic;
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

        public bool DeleteAuthor(int id)
        {
            return DeleteAuthorAction(id);
        }
        
        public List<AuthorDbTable> AuthorStats()
        {
            return GetAuthorsByPopularityAction();
        }
    }
}