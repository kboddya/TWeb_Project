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
        UserRegisterResult UserRegister(UDbTable data);
        UserRegisterResult UserLogin(UDbTable data);
    }

    public class UserRegisterResult
    {
        public bool Status { get; set; }

        public string StatusKey { get; set; }

        public string StatusMsg { get; set; }
    }
}