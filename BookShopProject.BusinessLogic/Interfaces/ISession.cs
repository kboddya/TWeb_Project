using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserAuthResult UserRegister(UDbTable data);
        UserAuthResult UserLogin(UDbTable data);
    }

    public class UserAuthResult
    {
        public bool Status { get; set; }

        public string StatusKey { get; set; }

        public string StatusMsg { get; set; }
    }
}