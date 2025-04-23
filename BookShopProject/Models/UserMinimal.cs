using System.Web.SessionState;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;

namespace BookShopProject.Models
{
    public class UserMinimal
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public URole Role { get; set; }

        public bool IsAuthenticated { get; set; } = false;

        public void SetSession(System.Web.HttpContext context)
        {
            if (context.Session["LoginStatus"] != "true") return;
            
            var user = context.GetMySessionObject();
            if (user == null) return;
            
            IsAuthenticated = true;
            Name = user.Name;
            Email = user.Email;
            Role = user.Role;
        }
    }
    
}