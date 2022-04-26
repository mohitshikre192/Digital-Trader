using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Invoicing.Controllers
{
    public class ProductsController : Controller
    {
        private readonly InvoiceDbContext _context;
        public ProductsController(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Db = _context.Products.Include(e => e.Brand); 
            return View(await Db.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand_Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,title,description,pur_price,sale_price,cur_stock,tax,BrandId,processor,Ram,Rom,primary_cam,front_cam,battery")] Product product)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand_Name", product.BrandId);
            return View(product);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) { return NotFound(); }
                var cu = await _context.Products.FindAsync(id);
                if (cu == null)
                {
                    return NotFound();
                }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand_Name", cu.BrandId);
            return View(cu);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,title,description,pur_price,sale_price,cur_stock,tax,BrandId,processor,Ram,Rom,primary_cam,front_cam,battery")] Product product)
            {
                if (id != product.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand_Name", product.BrandId);
            return View(product);
            }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(e => e.Brand).FirstOrDefaultAsync(m => m.Id == id);
            if (product== null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            return View(product);

        }
      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            return View(product);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProductExists(int id)
           
        {
                return _context.Products.Any(e => e.Id == id);
        }
    }
}

