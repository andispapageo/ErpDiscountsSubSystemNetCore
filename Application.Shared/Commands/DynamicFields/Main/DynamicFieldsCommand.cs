using Application.Shared.ViewModels;
using Application.Shared.ViewModels.TextAreaFields;
using MediatR;

namespace Application.Shared.Commands.DynamicFields.Main
{
    public record DynamicFieldsCommand : IRequest<Result>
    {
        public DynamicFieldsPostVm? DynamicFieldsPostVm { get; init; }
    }
}
