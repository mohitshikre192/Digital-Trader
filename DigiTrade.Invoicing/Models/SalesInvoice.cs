using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Models
{

    public class SalesInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "InvoiceNum")]
        public int Invoice_num { get; set; }
        //Invoice_Date- date(dd-mm-yyyy)
        [Display(Name = "Invoice Date")]
        [Required]
      
        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
         ApplyFormatInEditMode = true)]
     
        public DateTime Invoice_Date { get; set; }
        //Customer_id-Customer(cust_id) foreign key
        [Display(Name = "Customer ID")]
        [Required]
        public int Cust_ID { get; set; }
        [ForeignKey("Cust_ID")]
        public Customer Customers { get; set; }

        //Product_id-Product(ID)foreign key
        [Display(Name = "Product ID")]
        [Required]
        public int Product_ID { get; set; }
        [ForeignKey("Product_ID")]
        public Product Products{ get; set; }
        //Qty-int
        [Required]
        [Range(0, 100,ErrorMessage="Reaching individual limit or Enter positive numbers")]

        [Display(Name = "Quantity")]
        public int Qty { get; set; }

        //Rate-Product(sale_price)foreign key
        [Display(Name = "Rate")]
        [Required]
        [Range(0,100000,ErrorMessage ="Mustbe Positive")]
        public uint? Rate { get; set; }



    }
}
