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
    }
}
