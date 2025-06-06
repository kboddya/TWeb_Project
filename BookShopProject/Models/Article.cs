﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class ArticleList
    {
        public List<Article> Articles { get; set; }
    }
}