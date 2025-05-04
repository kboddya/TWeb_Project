using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic
{
    public class PublisherAdminBL: AdminApi, IPublisherAdmin
    {
        public PublishersList GetPublishers()
        {
            return PublishersListAction();
        }

        public PublisherDbTable GetPublisherById(int id)
        {
            return PublisherByIdAction(id);
        }

        public bool UpdatePublisher(PublisherDbTable publisher)
        {
            return EditPublisherAction(publisher);
        }

        public int CreatePublisher(PublisherDbTable publisher)
        {
            return AddPublisherAction(publisher);
        }

        public bool DeletePublisher(int id)
        {
            return DeletePublisherAction(id);
        }
    }
}