using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Invoicing.Controllers
{
  
        public class CustomersController : Controller
        {
            private readonly InvoiceDbContext _context;
            public CustomersController(InvoiceDbContext context)
            {
                _context = context;

            }
            public async Task<IActionResult> Index()
            {
                var Db = _context.Customers;
                return View(await Db.ToListAsync());
            }

            public async Task<IActionResult> Create([Bind("Id,Cust_Phone,Cust_Name,Cust_Email,Cust_Address")] Customer customer)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(customer);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) { return NotFound(); }
                var cu = await _context.Customers.FindAsync(id);
                if (cu == null)
                {
                    return NotFound();
                }
                return View(cu);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Cust_Phone,Cust_Name,Cust_Email,Cust_Address")] Customer customer)
            {
                if (id != customer.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.Id))
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
                return View(customer);
            }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer== null)
            {
                return NotFound();
            }

            return View(customer);

        }
      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CustomerExists(int id)
            {
                return _context.Customers.Any(e => e.Id == id);
            }
        }
    }
