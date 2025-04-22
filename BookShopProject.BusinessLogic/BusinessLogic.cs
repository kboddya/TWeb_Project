using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Interfaces;

namespace BookShopProject.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
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
    }
}
