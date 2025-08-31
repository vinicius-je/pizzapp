namespace PizzApp.Application.Features.OrderFeatures.Create
{
    public record CreateOrderItemRequest(Guid ProductId, int Quantity);
}
