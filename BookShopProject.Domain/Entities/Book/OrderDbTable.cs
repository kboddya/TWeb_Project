using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShopProject.Domain.Entities.Book
{
    public class OrderDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public DateTime LastUpdateTime { get; set; }

        [Required]
        public long ISBN { get; set; }

        [Required]
        public bool IsBought { get; set; }

        [Required]
        public decimal Price { get; set; }
    }

    public class OrdersList
    {
        public List<OrderDbTable> Orders { get; set; }
    }
}
