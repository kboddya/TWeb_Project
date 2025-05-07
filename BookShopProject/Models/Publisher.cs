using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class Publisher: UserMinimal
    {
        public Publisher(){}
        
        public Publisher(Domain.Entities.User.UserMinimal u): base(u){}
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Logo { get; set; }
        
        public int CountOfOrders { get; set; } = 0;
        
        public List<Book> Books { get; set; }
    }

    public class PublishersList: UserMinimal
    {
        public PublishersList(){}
        
        public PublishersList(Domain.Entities.User.UserMinimal u): base(u){}
        
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
    }
}