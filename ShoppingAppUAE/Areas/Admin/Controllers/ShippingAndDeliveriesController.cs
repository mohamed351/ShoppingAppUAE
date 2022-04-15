using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Data;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShippingAndDeliveriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingAndDeliveriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ShippingAndDeliveries
        public async Task<IActionResult> Index()
        {
            return View(await _context.shippingAndDeliveries.ToListAsync());
        }

        // GET: Admin/ShippingAndDeliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAndDelivery = await _context.shippingAndDeliveries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shippingAndDelivery == null)
            {
                return NotFound();
            }

            return View(shippingAndDelivery);
        }

        // GET: Admin/ShippingAndDeliveries/Create
        public IActionResult Create()
        {
            ViewBag.CountryCode = new SelectList(_context.Counties, "Code", "EnglishName");
            return View();
        }

        // POST: Admin/ShippingAndDeliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ShippingCountry,ShippingCompany,ShippingCompanyPhone,ShippingCompanyAddress,ShippingPrice,IsDefault,FirstWeightLimit,PriceAfterExcedingTheLimit,AdditionalKgAfterExcedingTheLimit")] ShippingAndDelivery shippingAndDelivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingAndDelivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shippingAndDelivery);
        }

        // GET: Admin/ShippingAndDeliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAndDelivery = await _context.shippingAndDeliveries.FindAsync(id);
            if (shippingAndDelivery == null)
            {
                return NotFound();
            }
            ViewBag.CountryCode = new SelectList(_context.Counties, "Code", "EnglishName");
            return View(shippingAndDelivery);
        }

        // POST: Admin/ShippingAndDeliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ShippingCountry,ShippingCompany,ShippingCompanyPhone,ShippingCompanyAddress,ShippingPrice,IsDefault,FirstWeightLimit,PriceAfterExcedingTheLimit,AdditionalKgAfterExcedingTheLimit")] ShippingAndDelivery shippingAndDelivery)
        {
            if (id != shippingAndDelivery.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingAndDelivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingAndDeliveryExists(shippingAndDelivery.ID))
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
            ViewBag.CountryCode = new SelectList(_context.Counties, "Code", "EnglishName");
            return View(shippingAndDelivery);
        }

        // GET: Admin/ShippingAndDeliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAndDelivery = await _context.shippingAndDeliveries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shippingAndDelivery == null)
            {
                return NotFound();
            }

            return View(shippingAndDelivery);
        }

        // POST: Admin/ShippingAndDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingAndDelivery = await _context.shippingAndDeliveries.FindAsync(id);
            _context.shippingAndDeliveries.Remove(shippingAndDelivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingAndDeliveryExists(int id)
        {
            return _context.shippingAndDeliveries.Any(e => e.ID == id);
        }
    }
}
