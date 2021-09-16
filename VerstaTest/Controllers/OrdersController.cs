using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VerstaTest.Models;
using VerstaTest.Models.Validation;

namespace VerstaTest.Controllers
{
    public class OrdersController : Controller
    {
        private readonly versta_testContext _context;
        private readonly IFieldRevalidator<decimal> _decimalRevalidator;
        public OrdersController(versta_testContext context, IFieldRevalidator<decimal> decimalRevalidator)
        {
            _context = context;
            _decimalRevalidator = decimalRevalidator;
        }

        public IActionResult CreateOrder()
        {
            return View("CreateOrder");
        }

        public async Task<IActionResult> OrderList()
        {
            return View("OrderList", await (from or in _context.Orders orderby or.CreationTime descending select or).ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(or => or.Id == id);
                if (order != null)
                    return View(order);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(Order order)
        {
            if (ModelState.IsValid ||
                _decimalRevalidator.TryReValidateField(ModelState, order, "PackageWeight",
                    (ord, packW) => ord.PackageWeight = packW, this))
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("OrderList");
            }
            return View("Edit", order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderCity,SenderAddress,ReceiverCity,ReceiverAddress,PackageWeight,ReceiveDate,CreationTime")]
            Order order)
        {
            if (ModelState.IsValid || 
                _decimalRevalidator.TryReValidateField(ModelState, order, "PackageWeight",
                    (ord, packW) => ord.PackageWeight = packW, this))
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return View("OrderCreated");
            }
            return View();
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
    }
}
