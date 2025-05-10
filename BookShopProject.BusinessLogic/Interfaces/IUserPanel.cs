using System.Collections.Generic;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IUserPanel
    {
        List<UDbTable> GetAll();
        UDbTable GetById(int id);
        void Update(UDbTable user);
        void Delete(int id);
    }
}