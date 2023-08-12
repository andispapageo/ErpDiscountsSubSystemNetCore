using Application.Shared.Commands.DynamicFields.Customer;
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
        readonly ILogger<HomeController> logger;
        readonly IMediator mediator;
        public HomeController(IMediator mediator, ILogger<HomeController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
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
                var resRequest = await mediator.Send(new CustomerPostDynamicHistoryFieldsCommand() { CustomerFieldsVms = inheritorPresenterVm.CustomerFields });
                logger.LogInformation("Customer Fields History updated {event}", resRequest.Succeeded);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}