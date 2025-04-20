using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BookShopProject.Domain.Entities.Order
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
        public decimal TotalPrice { get; set; }

        [Required]
        public string Status { get; set; }

        public List<ulong> ISBNs { get; set; }
    }
}
