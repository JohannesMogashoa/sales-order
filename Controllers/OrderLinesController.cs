using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Data;
using SalesOrder.Helpers;
using SalesOrder.Models;
using SalesOrder.Models.ViewModels;
using SalesOrder.Services;
using System.Diagnostics;

namespace SalesOrder.Controllers
{
    [Authorize]
    public class OrderLinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderLinesController> _logger;
        private readonly IOrderLineService _orderLineService;

        public OrderLinesController(ApplicationDbContext context, ILogger<OrderLinesController> logger, IOrderLineService orderLineService)
        {
            _context = context;
            _logger = logger;
            _orderLineService = orderLineService;
        }

        [HttpGet]
        public IActionResult AddOrderLine([FromRoute] string id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderLine([FromRoute] string id, AddOrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                string returnUrl = await _orderLineService.CreateOrderLineAsync(id, orderLine);
                
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(AddOrderLine), new { id });
        }

        [HttpGet]
        public IActionResult EditOrderLine([FromRoute] string id)
        {
            OrderLine orderLine = _context.OrderLines.Where(x => x.Id.Equals(id)).FirstOrDefault();

            if( orderLine != null && orderLine.Archived == false)
            {
                ViewBag.Id = orderLine.Id;
                EditOrderLine editOrderLineModel = ViewHelper.GetEditOrderLineView(orderLine);

                return View(editOrderLineModel);
            }

            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrderLine(EditOrderLine orderLine)
        {
            if(ModelState.IsValid)
            {
                string returnUrl = await _orderLineService.UpdateOrderLineAsync(orderLine);

                return Redirect(returnUrl);
            }
            return View(orderLine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrderLine([FromRoute] string id)
        {
            if(ModelState.IsValid)
            {
                string returnUrl = await _orderLineService.DeleteOrderLineAsync(id);

                return Redirect(returnUrl);
            }
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
