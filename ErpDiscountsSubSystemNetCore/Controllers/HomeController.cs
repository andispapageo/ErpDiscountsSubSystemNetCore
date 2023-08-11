using Application.Shared.Commands;
using Application.Shared.Commands.Orders;
using Domain.Core.Entities;
using ErpDiscountsSubSystemNetCore.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ErpDiscountsSubSystemNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IMediator Mediator { get; }

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            Mediator = mediator;
        }

        public async Task<ActionResult<IEnumerable<TbOrder>>> Index()
        {
            var getOrdersFromCustomer = await Mediator.Send(new OrderCommand() { CustomerId = 1 });
            return View(getOrdersFromCustomer);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}