using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopProject.Domain.Entities.Publisher
{
    public class PublisherDbTable
    {
        [Key]
        [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Logo { get; set; }
        
        public int CountOfOrders { get; set; } = 0;
    }
    
    public class PublishersList
    {
        public List<PublisherDbTable> Publishers { get; set; } = new List<PublisherDbTable>();
    }
}