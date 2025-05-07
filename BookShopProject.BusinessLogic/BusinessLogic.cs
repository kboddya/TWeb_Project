using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IOrder GetOrderAdminBL()
        {
            return new OrderAdminBL();
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
    }
}
