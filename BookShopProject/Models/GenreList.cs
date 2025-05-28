using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class GenreList: UserMinimal
    {
        public GenreList(){}
        
        public GenreList(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        
        public List<string> Genres { get; set; }
        
        public int CountOfOrders { get; set; } = 0;
    }
}