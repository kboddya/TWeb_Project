using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Domain.Enums.User;

namespace BookShopProject.BusinessLogic.Core
{
    public class AdminApi
    {

        public List<UDbTable> GetAllUsersAction()
        {
            List<UDbTable> u;

            using (var db = new UserContext())
            {
                u = db.Users.ToList();
            }

            return u;
        }

        public UDbTable GetUserByIdAction(int id)
        {
            UDbTable u;
            using (var db = new UserContext())
            {
                u = db.Users.FirstOrDefault(x => x.Id == id);
            }

            return u;
        }

        public void DeleteUserAction(int id)
        {
            using (var db = new UserContext())
            {
                UDbTable u = db.Users.FirstOrDefault(y => y.Id == id);
                if (u != null)
                {
                    db.Users.Remove(u);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateUserRoleAction(UDbTable userToUpdate)
        {
            using (var db = new UserContext())
            {
                UDbTable u = db.Users.FirstOrDefault(x => x.Id == userToUpdate.Id);
                if (u != null)
                {
                    db.Users.AddOrUpdate(userToUpdate);
                    db.SaveChanges();
                }
            }
        }

    }
}
