namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IMessageUser
    {
        bool SendMessageToAdmin(Domain.Entities.User.MessageDbTable message);
    }
}