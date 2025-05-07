using System.Collections.Generic;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.Models
{
    public class BookList: UserMinimal
    {
        public BookList(){}
        
        public BookList(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        
        public string NameOfList { get; set; }
        
        public List<Book> Products { get; set; }
        
        public string parameterForSearch { get; set; }
        
        public BSearchParameter typeOfSearch { get; set; }  
    }
}