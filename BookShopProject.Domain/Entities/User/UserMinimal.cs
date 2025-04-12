using BookShopProject.Domain.Enums.User;

namespace BookShopProject.Domain.Entities.User
{
    public class UserMinimal
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public URole Role { get; set; }
    }
}