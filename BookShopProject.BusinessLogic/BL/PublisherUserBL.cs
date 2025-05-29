using System.Collections.Generic;
using System.Security.Policy;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic
{
    public class PublisherUserBL: UserApi, IPublisherUser
    {
        public PublisherDbTable GetPublisherById(int id)
        {
            return PublisherByIdAction(id);
        }
        
        public BookListDb GetBooksByPublisherId(int id)
        {
            return BookByPublisherIdAction(id);
        }
        
        public List<PublisherDbTable> GetPublishersByPopularity()
        {
            return GetPublishersByPopularityAction();
        }
    }
}