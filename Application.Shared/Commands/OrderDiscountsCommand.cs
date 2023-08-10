using Application.Shared.ViewModels;
using MediatR;

namespace Application.Shared.Commands
{

    public record OrderDiscountsCommand : IRequest<IEnumerable<OrderVm>>
    {
        public int? CustomerId { get; init; }
    }
}
