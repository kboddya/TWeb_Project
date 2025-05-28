using System;

namespace BookShopProject.Models
{
    public class MessageForAdmin: UserMinimal
    {
        public MessageForAdmin()
        {
        }
        
        public MessageForAdmin(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        
        public int Id { get; set; }
        
        public string Subject { get; set; }
        
        public string Message { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public bool IsRead { get; set; } = false;
    }
}