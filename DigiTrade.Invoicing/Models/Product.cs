using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Models
{ public class Product {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        [MinLength(2, ErrorMessage = "Title should be 2 to 128 Characters")]
        [Display(Name = "Product Title")]
        public string title { get; set; }

        //description- varchar(255)
        [Required]
        [StringLength(255)]
        [MinLength(10, ErrorMessage = "Description should be 2 to 255 Characters")]
        [Display(Name = "Description")]
        public string description { get; set; }
        //pur_price- int (int)
        

        [Display(Name = "Sales Price")]
        [Required(ErrorMessage = "Field is Required")]
        [Range(1, 1000000, ErrorMessage = "Must be Positive")]

        public uint sale_price { get; set; }
        //cur_stock- int (stock) (smallint)
        [Required(ErrorMessage = "Required field Must be Positive")]
       
        [Display(Name = "Current Stock")]
        [Range(0, 100000, ErrorMessage = "Must be Positive")]
        public uint cur_stock { get; set; }
        //tax- int (tinyint)
       [Range(0,100,ErrorMessage ="Tax value must be under 100 & positive")]
        public byte? tax { get; set; }
        //brand-Brand(Brand_name) foreign key varchar(128)
        
        [DisplayFormat(NullDisplayText = "No Brand")]
        [Display(Name = "Brand")]
    public int? BrandId{ get; set; }
        [Display(Name ="Brand")]
        [ForeignKey("BrandId")]
    public Brand Brand { get; set; }
        //processor-varchar(128)
        [Required]
        [StringLength(128)]
        [MinLength(2, ErrorMessage = "Processor should be of 2 to 128 Characters")]
        [Display(Name = "Processor")]
        public string processor { get; set; }
        //Ram-int (smallint)
        [Required]
        [Range(0,100,ErrorMessage ="RAM value must be postive in GB")]
        [Display(Name = "RAM GB")]
        public short Ram { get; set; }
        //Rom-int (smallint)
        [Required]
        [Range(0, 2000, ErrorMessage = "ROM value must be postive in GB")]

        [Display(Name = "ROM GB")]
        public short Rom { get; set; }
        //primary_cam-int (tinyint)
        [Required]
        [Range(0, 300, ErrorMessage = "Camera value must be postive in MP")]
        [Display(Name = "Primary Camera")]
        public byte primary_cam { get; set; }
        //front-cam-int (tinyint)
        [Required]
        [Range(0, 300, ErrorMessage = "Camera value must be postive in MP")]

        [Display(Name = "Front Camera")]
        public byte front_cam { get; set; }

        //battery- int (smallint)
        [Required]
        [Range(0, 12000, ErrorMessage = "Battery value must be postive in mAh")]

        [Display(Name = "Battery mAh")]
        public short battery { get; set; }

}
}
