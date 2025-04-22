using System.Collections.Generic;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.Models
{
    public class BookList
    {
        public string NameOfList { get; set; }
        public List<Book> Products { get; set; }
        
        public string parameterForSearch { get; set; }
        
        public BSearchParameter typeOfSearch { get; set; }  
    }
}