using Application.Shared.ViewModels;
using MediatR;

namespace Application.Shared.Commands.Orders
{
    public record InheritorPresenterCommand : IRequest<InheritorPresenterVm>
    {
        public int? CustomerId { get; init; }
    }
}
