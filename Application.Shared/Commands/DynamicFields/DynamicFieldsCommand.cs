using Application.Shared.ViewModels;
using Application.Shared.ViewModels.TextAreaFields;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Commands.DynamicFields
{
    public class DynamicFieldsCommand : IRequest<Result>
    {
        public DynamicFieldsPostVm? DynamicFieldsPostVm { get; init; }
    }
}
