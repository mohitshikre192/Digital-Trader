using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Models
{
  
  
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [StringLength(128)]
        [Required]
        [MinLength(2, ErrorMessage = "Full Name should be 2 to 128 Characters")]
        [Display(Name = "Customer Name")]
        public string Cust_Name { get; set; }

        
        [Phone]
        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "10 digits numbers only - Example: 9512341234")]
        [StringLength(10)]
        [Display(Name = "Customer Phone")]
        public string Cust_Phone { get; set; }

        
        [EmailAddress]
        [Required]
        [StringLength(128,ErrorMessage ="Email can be maximum 128 Characters")]
        [Display(Name = "Customer Email")]
        public string Cust_Email { get; set; }
        
        [StringLength(128, ErrorMessage = "Address can be maximum 128 Characters only !")]
        [Required]
        [Display(Name = "Customer Address")]
        public string Cust_Address { get; set; }

    }
   
}
