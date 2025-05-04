using System.Web.SessionState;
using System.Web.WebPages;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;

namespace BookShopProject.Models
{
    public class UserMinimal
    {
        public UserMinimal(Domain.Entities.User.UserMinimal u)
        {
            if (u == null)
            {
                IsAuthenticated = false;
                return;
            }
            Name = u.Name;
            Email = u.Email;
            Role = u.Role;
            if (!Email.IsEmpty())
            {
                IsAuthenticated = true;
            }
        }

        protected UserMinimal()
        {
            IsAuthenticated = false;
        }

        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public URole Role { get; set; }

        public bool IsAuthenticated { get; set; } = false;
    }
    
}