using PizzApp.Domain.Entities;

namespace PizzApp.Application.Features.OrderFeatures.Create
{
    public interface ICreateOrderService
    {
        Task<Result> Execute(CreateOrderRequest request, CancellationToken cancellation);
    }
}
