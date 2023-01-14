using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Data;
using SalesOrder.Models;
using SalesOrder.Models.RestModels;

namespace SalesOrder.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve all order headers
        /// </summary>
        /// <response code="200">Returns order headers</response>
        /// <response code="500">Internal Server Error - Something went wrong</response>
        /// <returns>Returns Order headers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderHeaderRestModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            List<OrderHeader> orders = await _context.OrderHeaders
                .Where(o => o.Archived == false)
                .ToListAsync();

            List<OrderHeaderRestModel> ordersRestModel = new();

            foreach(OrderHeader orderHeader in orders)
            {
                ordersRestModel.Add(_mapper.Map<OrderHeaderRestModel>(orderHeader));
            }

            return Ok(ordersRestModel);
        }

        /// <summary>
        /// Retrieve Order by order number
        /// </summary>
        /// <response code="200">Returns order</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Order could not be found</response>
        /// <response code="500">Internal Server Error - Something went wrong</response>
        /// <returns>Returns Order</returns>
        [HttpGet("{orderNumber}")]
        [ProducesResponseType(typeof(OrderHeaderRestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] string orderNumber)
        {
            OrderHeader order = _context.OrderHeaders
                .Where(o => o.OrderNumber == orderNumber && o.Archived == false)
                .Include(o => o.OrderLines.Where(oL => oL.Archived == false))
                .FirstOrDefault();

            if(order == null || order.Archived == true)
            {
                return NotFound(new ErrorRestModel
                {
                    Reason = "Order Header not found",
                    Errors = new List<string> { $"The Order Header with order number: {orderNumber}, was not found. Please supply a valid order number and try again." }
                });
            }

            OrderHeaderRestModel restModel = _mapper.Map<OrderHeaderRestModel>(order);
            restModel.OrderLines = new List<OrderLineRestModel>();

            foreach(OrderLine line in order.OrderLines)
            {
                restModel.OrderLines.Add(_mapper.Map<OrderLineRestModel>(line));
            }

            return Ok(restModel);
        }
    }
}
