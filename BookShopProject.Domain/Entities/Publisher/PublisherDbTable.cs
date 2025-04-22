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
    }
}