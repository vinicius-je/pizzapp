namespace PizzApp.Application.Features.OrderFeatures.Create
{
    public record CreateOrderRequest(Guid CustomerId, ICollection<CreateOrderItemRequest> Items);
}
