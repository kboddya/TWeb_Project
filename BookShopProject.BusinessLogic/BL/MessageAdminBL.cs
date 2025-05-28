using System.Collections.Generic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
{
    public class MessageAdminBL: AdminApi, IMessageAdmin
    {
        public MessageDbTable GetMessageById(int id)
        {
            return GetMessageByIdAction(id);
        }

        public List<MessageDbTable> GetMessages()
        {
            return GetMessagesAction();
        }
    }
}