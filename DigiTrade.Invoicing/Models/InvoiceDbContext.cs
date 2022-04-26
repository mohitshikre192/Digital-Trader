using Microsoft.EntityFrameworkCore;
namespace Invoicing.Models
{
    public class InvoiceDbContext : DbContext
    {
        
            public InvoiceDbContext (DbContextOptions<InvoiceDbContext> options) :
           base(options)
            {

            }
       
        
        public DbSet<Product> Products { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<SalesInvoice> SalesInvoices { get; set; }

        }
    }
