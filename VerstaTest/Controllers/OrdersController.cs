using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VerstaTest.Models.DTOEntities;
using VerstaTest.Models.Entities;
using VerstaTest.Models.Validation;
using VerstaTest.Repository;
using VerstaTest.Services;

namespace VerstaTest.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult CreateOrder()
        {
            return View("CreateOrder");
        }

        public async Task<IActionResult> OrderList()
        {
            return View("OrderList", await orderService.GetOrders());
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
                Order order = await orderService.GetOrder(id.Value);
                if (order != null)
                    return View(order);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(Order order)
        {
            if(await orderService.UpdateOrder(order, ModelState, this)){
                return RedirectToAction("OrderList");
            }
            return View("Edit", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Order order)
        {
            if (await orderService.InsertOrder(order, ModelState, this))
            {
                return View("OrderCreated");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !await orderService.DeleteOrder(id.Value))
                return NotFound();
            return RedirectToAction("OrderList");
        }
    }
}
