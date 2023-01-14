using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesOrder.Models;
using SalesOrder.Models.ViewModels;
using SalesOrder.Services;
using System.Diagnostics;

namespace SalesOrder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderHeaderService _headerService;

        public HomeController(IOrderHeaderService orderHeaderService)
        {
            _headerService = orderHeaderService;
        }

        public IActionResult Index()
        {
            IndexViewModel dogImage = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dog.ceo/api/");
                var responseTask = client.GetAsync("breeds/image/random");
                responseTask.Wait();
                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    dogImage = JsonConvert.DeserializeObject<IndexViewModel>(response);
                }
            }
            return View(dogImage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Orders()
        {
            List<OrderHeader> orderHeaders = _headerService.GetOrderHeaders();

            return View(orderHeaders);
        }

        [HttpGet("/Home/Orders/{id}")]
        public IActionResult Order([FromRoute] string id)
        {
            OrderHeader order = _headerService.GetOrderHeader(id);

            return View(order);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}