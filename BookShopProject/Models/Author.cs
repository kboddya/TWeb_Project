using System;
using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public DateTime DeathDate { get; set; }
        
        public string Wiki { get; set; }
        
        public List<string> Genres { get; set; }
        
        public List<Book> Books { get; set; }
    }
    
    public class AuthorList
    {
        public List<Author> Authors { get; set; }
    }
}