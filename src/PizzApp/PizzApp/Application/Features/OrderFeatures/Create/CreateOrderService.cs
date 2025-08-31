using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;

namespace PizzApp.Application.Features.OrderFeatures.Create
{
    public class CreateOrderService : ICreateOrderService
    {
        protected readonly IOrderRepository _orderRepository;
        protected readonly ICustomerRepository _customerRepository;
        protected readonly IProductRepository _productRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public CreateOrderService(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IOrderRepository orderRepository, 
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public string CustomerErrorMessage = "Customer not registered in the system";
        public string InvalidItemQuantityError = "The item quantity must be greater than zero";
        public string OrderCreatedMessage = "Customer Order created with sucessful";

        public async Task<Result> Execute(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var result = new Result();

            bool hasItemWithInvalidQuantity = request.Items.Any(x => x.Quantity <= 0);

            if (hasItemWithInvalidQuantity)
            {
                result.Error(InvalidItemQuantityError);
                return result;
            }

            Customer? customer = await _customerRepository.GetCustomerById(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                result.Error(CustomerErrorMessage);
                return result;
            }

            var order = new Order(request.CustomerId, new List<OrderItem>());
            result = await AddOrderItems(request, result, order, cancellationToken);

            if (result.IsSuccess)
            {
                await _orderRepository.Create(order);
                await _unitOfWork.Commit(cancellationToken);
                result.Success(OrderCreatedMessage, order.Id);
            }

            return result;
        }

        private async Task<Result> AddOrderItems(CreateOrderRequest request, Result result, Order order, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetProductById(item.ProductId, cancellationToken);

                if (product == null)
                {
                    result.Error($"Item with ID {item.ProductId} does not exist on database");
                    return result;
                }

                order.AddItem(new OrderItem(product.Id, item.Quantity, product.Price));
            }

            return result;
        }
    }
}
