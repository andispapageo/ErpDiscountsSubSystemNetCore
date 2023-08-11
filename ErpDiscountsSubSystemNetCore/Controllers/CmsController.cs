using Application.Shared.Commands.DynamicFields;
using Application.Shared.ViewModels;
using Application.Shared.ViewModels.TextAreaFields;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpDiscountsSubSystemNetCore.Controllers
{
    [Authorize]
    public class CmsController : Controller
    {
        public CmsController(IMediator mediator)
        {
            Mediator = mediator;
        }
        IMediator Mediator { get; }


        [HttpGet]
        public IActionResult Index()
        {
            return View(new DynamicFieldsPostVm());
        }


        [HttpPost]
        public async Task<ActionResult<object>> OnPost(DynamicFieldsPostVm dynamicFieldsPostVm)
        {
            try
            {
                var saveRequestResult = await Mediator.Send(new DynamicFieldsCommand() { DynamicFieldsPostVm = dynamicFieldsPostVm });
                return View(new DynamicFieldsPostVm() { Result = saveRequestResult });
            }
            catch (Exception ex)
            {
                var result = new Result(false, new[] { ex.Message });
                return View(new DynamicFieldsPostVm() { Result = result });
            }
        }
    }
}
