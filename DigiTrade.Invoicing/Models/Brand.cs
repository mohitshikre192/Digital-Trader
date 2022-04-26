using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Models
{
    public class Brand
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //primary key auto generated
        [Required]
        [StringLength(128)]
        [MinLength(2, ErrorMessage = "Brand Name should be 2 to 128 Characters")]
        [Display(Name = "Brand Name")]
        public string Brand_Name { get; set; }
    }
}
