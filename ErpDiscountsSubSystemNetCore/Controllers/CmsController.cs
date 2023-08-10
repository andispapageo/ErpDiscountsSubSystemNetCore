using Application.Shared.ViewModels.TextAreaFields;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpDiscountsSubSystemNetCore.Controllers
{
    [Authorize]
    public class CmsController : Controller
    {
        public CmsController(ILogger<CmsController> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        ILogger<CmsController> Logger { get; }
        IMediator Mediator { get; }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public void OnPost(IEnumerable<DynamicFieldsVm> dynamicFieldsVms)
        {
           
        }
    }
}
