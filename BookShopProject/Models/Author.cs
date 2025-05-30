﻿using System;
using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class Author: UserMinimal
    {
        public Author(){}
        
        public Author(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Image { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public DateTime DeathDate { get; set; }
        
        public string Wiki { get; set; }
        
        public List<string> Genres { get; set; } = new List<string>();

        public List<Book> Books { get; set; } = new List<Book>();
        
        public int CountOfOrders { get; set; } = 0;
    }
    
    public class AuthorList: UserMinimal
    {
        public AuthorList(){}
        
        public AuthorList(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}