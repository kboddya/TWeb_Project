﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IOrderAdmin GetOrderAdminBL()
        {
            return new OrderAdminBL();
        }

        public IOrderUser GetOrderUserBL()
        {
            return new OrderUserBL();
        }

        public IAuthorUser GetAuthorUserBL()
        {
            return new AuthorUserBL();
        }

        public IAuthorAdmin GetAuthorAdminBL()
        {
            return new AuthorAdminBL();
        }

        public IBookAdmin GetBookAdminBL()
        {
            return new BookAdminBL();
        }

        public IBookUser GetBookUserBL()
        {
            return new BookUserBL();
        }

        public IPublisherAdmin GetPublisherAdmin()
        {
            return new PublisherAdminBL();
        }

        public IPublisherUser GetPublisherUserBL()
        {
            return new PublisherUserBL();
        }

        public IPublisherPublisher GetPublisherPublisherBL()
        {
            return new PublisherPublisherBL();
        }

        public IArticleUser GetArticleUserBL()
        {
            return new ArticleUserBL();
        }

        public IArticleAdmin GetArticleAdminBL()
        {
            return new ArticleAdminBL();
        }

        public IUserPanel GetUserPanelBL()
        {
            return new UserPanelBL();
        }

        public IMessageUser GetMessageUserBL()
        {
            return new MessageUserBL();
        }

        public IMessageAdmin GetMessageAdminBL()
        {
            return new MessageAdminBL();
        }
    }
}