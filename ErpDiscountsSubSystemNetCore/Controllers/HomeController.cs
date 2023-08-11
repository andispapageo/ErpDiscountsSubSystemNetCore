using Application.Shared.Commands.Orders;
using Application.Shared.ViewModels;
using Domain.Core.Entities;
using ErpDiscountsSubSystemNetCore.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ErpDiscountsSubSystemNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IMediator mediator { get; }
        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ActionResult<IEnumerable<TbOrder>>> Index()
        {
            var getOrdersFromCustomer = await mediator.Send(new InheritorPresenterCommand() { CustomerId = 1 });
            return View(getOrdersFromCustomer);
        }

        [HttpPost]
        public async Task<ActionResult> OnPostFields(InheritorPresenterVm inheritorPresenterVm)
        {
            if (ModelState.IsValid)
            {
                var resRequest = await mediator.Send()
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}