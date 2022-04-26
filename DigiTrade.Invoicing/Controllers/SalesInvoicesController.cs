using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Controllers
{
    public class SalesInvoicesController : Controller
    {
        private readonly InvoiceDbContext _context;
        public SalesInvoicesController(InvoiceDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index_Invoices()
        {
            var Db = _context.SalesInvoices.Include(e => e.Products).Include(e => e.Customers);
            return View(await Db.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["Rate"] = new SelectList(_context.Products, "Id", "pur_price");
            ViewData["Cust_ID"] = new SelectList(_context.Customers, "Id", "Cust_Name");
            ViewData["Product_ID"] = new SelectList(_context.Products, "Id", "title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Invoice_num,Invoice_Date,Cust_ID,Product_ID,Qty,Rate")] SalesInvoice si)
        {
            if (ModelState.IsValid)
            {
                _context.Add(si);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index_Invoices));
            }
            ViewData["Rate"] = new SelectList(_context.Products, "Id", "pur_price",si.Rate);
            ViewData["Cust_ID"] = new SelectList(_context.Customers, "Id", "Cust_Name",si.Cust_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "Id", "title",si.Product_ID);
            return View(si);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }
            var cu = await _context.SalesInvoices.FindAsync(id);
            if (cu == null)
            {
                return NotFound();
            }
            ViewData["Rate"] = new SelectList(_context.Products, "Id", "pur_price", cu.Rate);
            ViewData["Cust_ID"] = new SelectList(_context.Customers, "Id", "Cust_Name", cu.Cust_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "Id", "title", cu.Product_ID);
            return View(cu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Invoice_num,Invoice_Date,Cust_ID,Product_ID,Qty,Rate")] SalesInvoice salesinvoice)
        {
            if (id != salesinvoice.Invoice_num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesinvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesInvoiceExists(salesinvoice.Invoice_num))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index_Invoices));
            }
            ViewData["Rate"] = new SelectList(_context.Products, "Id", "pur_price", salesinvoice.Rate);
            ViewData["Cust_ID"] = new SelectList(_context.Customers, "Id", "Cust_Name", salesinvoice.Cust_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "Id", "title", salesinvoice.Product_ID);
            return View(salesinvoice);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesinvoice = await _context.SalesInvoices.Include(e => e.Products).Include(e => e.Customers).FirstOrDefaultAsync(m => m.Invoice_num == id);
            if (salesinvoice == null)
            {
                return NotFound();
            }
            ViewData["Rate"] = new SelectList(_context.Products, "Id", "pur_price", salesinvoice.Rate);
            ViewData["Cust_ID"] = new SelectList(_context.Customers, "Id", "Cust_Name", salesinvoice.Cust_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "Id", "title", salesinvoice.Product_ID);
            return View(salesinvoice);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesinvoice = await _context.SalesInvoices.Include(e => e.Products).Include(e => e.Customers)
                .FirstOrDefaultAsync(m => m.Invoice_num == id);
            if (salesinvoice == null)
            {
                return NotFound();
            }
            ViewData["Rate"] = new SelectList(_context.Products, "Id", "pur_price", salesinvoice.Rate);
            ViewData["Cust_ID"] = new SelectList(_context.Customers, "Id", "Cust_Name", salesinvoice.Cust_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "Id", "title", salesinvoice.Product_ID);
            return View(salesinvoice);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesinvoice = await _context.SalesInvoices.FindAsync(id);
            _context.SalesInvoices.Remove(salesinvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index_Invoices));
        }
        private bool SalesInvoiceExists(int id)

        {
            return _context.SalesInvoices.Any(e => e.Invoice_num == id);
        }

    //    public JsonResult getAll()
    //    {
    //        return Json(_context, JsonRequestBehavior.AllowGet);
    //    }


    //    [HttpPost]
    //public ActionResult GenerateReport(int Id)
    //{
    //    string html = "";
    //    SalesInvoice invoice = _context.SalesInvoices.Where(x => x.Invoice_num == Id).FirstOrDefault();
    //    StringBuilder sb = new StringBuilder();
    //    // Company Logo and Name.
    //    html += "<div style='text-align:center;'>" +
    //            "<img src='" + GetUrl("Files/Logo.png") + "' height='50px' width='100px' />" +
    //            "<br/><h2>Excelasoft Solutions</h2><hr/>" +
    //            "</div>";
 
    //    // Customer Data.
    //    html += "<table>" +
    //            "<tr><th>date</th><td colspan='2'> : " + invoice.Invoice_Date + "</td></tr>"+
    //            "<tr><th>Invoice number</th><td colspan='2'> : " + invoice.Invoice_num + "</td></tr>" +
    //            "<tr><th>Customer Name</th><td colspan='2'> : " + invoice.Customers.Cust_Name + "</td></tr>" +
    //            "<tr><th>Email</th><td colspan='2'> : " + invoice.Customers.Cust_Email + "</td></tr>" +
    //            "<tr><th>Phone No</th><td colspan='2'> : " + invoice.Customers + "</td></tr>" +
    //            "<tr><th>Product title</th><th>Quantity</th><th>Rate </th><td colspan='2'> : " + invoice.Products.title  + "</td><td colspan='2'> : " + invoice.Qty + "</td><td colspan='2'> : " + invoice.Rate + "</td></tr>" +
    //             "<tr><th>Total</th><td colspan='2'> : " + (invoice.Rate)*(invoice.Qty) + "</td></tr>" +
    //            "</table>";
 
    //    using (MemoryStream stream = new MemoryStream())
    //    {
    //        StringReader sr = new StringReader(html);
    //        Document pdfDoc = new Document(PageSize.A4, 50f, 10f, 30f, 10f);
 
    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
    //        pdfDoc.Open();
    //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
    //        pdfDoc.Close();
    //        TempData["Data"] = stream.ToArray();
    //    }
 
    //    return new JsonResult() { Data = new { FileName = invoice.Customers.Cust_Name.Trim().Replace(" ", "_") + ".pdf" } };
    //}
 
    //[HttpGet]
    //public virtual ActionResult Download(string filename)
    //{
    //    if (TempData["Data"] != null)
    //    {
    //        byte[] data = TempData["Data"] as byte[];
    //        return File(data, "application/pdf", filename);
    //    }
    //    else
    //    {
    //        return new EmptyResult();
    //    }
    //}
 
    //private string GetUrl(string imagePath)
    //{
    //    string appUrl = System.Web.HttpRuntime.AppDomainAppVirtualPath;
    //    if (appUrl != "/")
    //    {
    //        appUrl = "/" + appUrl;
    //    }
    //    string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, appUrl);
 
    //    return url + imagePath;
    //}

    }
}
