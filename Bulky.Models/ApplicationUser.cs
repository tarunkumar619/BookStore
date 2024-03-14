using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



namespace Bulky.Models
{
    public class ApplicationUser : IdentityUser
    {
    
        [Required]
        public String Name { get; set; }    
        public string? StreetAddress { get; set; }   
        public string? City { get; set; }   
        public string? State { get; set; } 
        public string? PostalCode { get; set; }
        
        public int ? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company company { get; set; }    

    }
}
