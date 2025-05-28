using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopProject.Domain.Entities.Book
{
    public class ReviewDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public long ISBN { get; set; }
    }
}