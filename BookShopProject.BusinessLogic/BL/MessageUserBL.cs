using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
{
    public class MessageUserBL: UserApi, IMessageUser
    {
        public bool SendMessageToAdmin(MessageDbTable message)
        {
            return SendMessageToAdminAction(message);
        }
    }
}