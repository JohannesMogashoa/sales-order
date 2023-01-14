using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Data;
using SalesOrder.Helpers;
using SalesOrder.Models;
using SalesOrder.Models.ViewModels;
using SalesOrder.Services;
using System.Diagnostics;

namespace SalesOrder.Controllers
{
    [Authorize]
    public class OrderHeadersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderHeadersController> _logger;
        private readonly IOrderHeaderService _orderHeaderService;

        public OrderHeadersController(ApplicationDbContext context, ILogger<OrderHeadersController> logger, IOrderHeaderService orderHeaderService)
        {
            _context = context;
            _logger = logger;
            _orderHeaderService = orderHeaderService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderHeader order)
        {
            if(ModelState.IsValid)
            {
                var newOrder = await _orderHeaderService.CreateOrderHeaderAsync(order);

                return Redirect($"/Home/Orders/{newOrder.Id}");
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] string id)
        {
            var order = _context.OrderHeaders.Include(x => x.OrderLines).FirstOrDefault(x => x.Id == id);

            if(order == null || order.Archived == true)
            {
                return Redirect("/Home/Orders");
            }

            ViewBag.ID = order.Id;

            var editOrderModel = ViewHelper.GetEditOrderHeaderView(order);

            return View(editOrderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditOrderHeader order)
        {
            if(ModelState.IsValid)
            {
                await _orderHeaderService.EditOrderHeaderAsync(order);

                return Redirect("/Home/Orders");
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrderHeader([FromRoute] string id)
        {
            if(ModelState.IsValid)
            {
                await _orderHeaderService.DeleteOrderHeaderAsync(id);

                return Redirect("/Home/Orders");
            }
            return Redirect("/Home/Orders");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
