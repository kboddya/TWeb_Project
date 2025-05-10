using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopProject.Domain.Entities.Articles
{
    public class ArticleDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public string Image { get; set; }
    }
}