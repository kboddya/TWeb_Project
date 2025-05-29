using System.Collections.Generic;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IPublisherAdmin
    {
        PublishersList GetPublishers();
        
        PublisherDbTable GetPublisherById(int id);
        
        bool UpdatePublisher(PublisherDbTable publisher);
        
        int CreatePublisher(PublisherDbTable publisher);
        
        bool DeletePublisher(int id);
        
        List<PublisherDbTable> PublisherStats();
    }
}