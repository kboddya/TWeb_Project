using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopProject.Domain.Entities.User
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId {get;set;}
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string CookieString { get; set; }
        
        [Required]
        public DateTime ExpireTime { get; set; }
    }
}