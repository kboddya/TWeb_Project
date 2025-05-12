using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
{
    public class UserPanelBL : AdminApi, IUserPanel
    {
        public void Delete(int id)
        {
            DeleteUserAction(id);
        }

        public List<UDbTable> GetAll()
        {
            return GetAllUsersAction();
        }

        public UDbTable GetById(int id)
        {
            return GetUserByIdAction(id);
        }

        public void Update(UDbTable user)
        {
            UpdateUserRoleAction(user);
        }
    }
}
