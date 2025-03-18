using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.User;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserLoginResult UserLogin(ULoginData data);
    }

    public class UserLoginResult
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
    }
}