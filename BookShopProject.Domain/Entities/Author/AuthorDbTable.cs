using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopProject.Domain.Entities.Author
{
    public class AuthorDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [Display(Name = "Image")]
        public string Image { get; set; }
        
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Death Date")]
        public DateTime DeathDate { get; set; }
        
        [Display(Name = "Wiki")]
        public string Wiki { get; set; }
        
        [Display(Name = "Genres")]
        public List<string> Genres { get; set; }
        
        public DateTime LastUpdateTime { get; set; }
    }

    public class AuthorsList
    {
        public List<AuthorDbTable> Authors { get; set; }
    }
}