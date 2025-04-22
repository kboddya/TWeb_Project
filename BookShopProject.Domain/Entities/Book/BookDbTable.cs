using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopProject.Domain.Entities.Book
{
    public class BookDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int AuthorId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(100)]
        public string AuthorFirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string AuthorLastName { get; set; }

        [Required] public long ISBN { get; set; } = 9780000000000;
        
        [Required]
        public decimal Price { get; set; }
        
        public decimal DiscountedPrice { get; set; } = decimal.MinusOne;
        
        public string Description { get; set; }
        
        public string Image { get; set; }
        
        public int Count { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Language { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Genre { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Publisher { get; set; }
        
        [Required]
        public int PublisherId { get; set; }
        
        [Required]
        public DateTime PublishDate { get; set; }
        
        [Required]
        public DateTime LastUpdateTime { get; set; }
        
        public int CountOfOrders { get; set; } = -1;
    }
    
    public class BookListDb
    {
        public List<BookDbTable> Books { get; set; }
    }
}