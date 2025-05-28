using System.Collections.Generic;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IMessageAdmin
    {
        MessageDbTable GetMessageById(int id);
        
        List<MessageDbTable> GetMessages();
    }
}