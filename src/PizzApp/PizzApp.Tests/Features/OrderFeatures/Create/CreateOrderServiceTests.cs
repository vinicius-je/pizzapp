using Moq;
using PizzApp.Application.Features.OrderFeatures.Create;
using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;

namespace PizzApp.Application.Tests.Features.OrderFeatures.Create
{
    [TestFixture]
    public class CreateOrderServiceTests
    {
        protected Mock<IOrderRepository> _orderRepositoryMock = null!;
        protected Mock<ICustomerRepository> _customerRepositoryMock = null!;
        protected Mock<IProductRepository> _productRepositoryMock = null!;
        protected Mock<IUnitOfWork> _unitOfWorkMock = null!;
        protected CreateOrderService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _service = new CreateOrderService(
                _customerRepositoryMock.Object, 
                _productRepositoryMock.Object, 
                _orderRepositoryMock.Object, 
                _unitOfWorkMock.Object);
        }

        [Test]
        public async Task CreateOrder_ShouldReturn_Success()
        {
            // Arrange
            var customer = new Customer("Walter White", "walterwhite@email.com", "99338844");

            var product1 = new Product("Margherita Pizza", "Classic pizza with tomatoes, mozzarella, and basil", 29.90m, ProductCategoryEnum.PIZZA);
            var product2 = new Product("Coca-Cola 350ml", "Refreshing soda can", 5.90m, ProductCategoryEnum.DRINK);
            var product3 = new Product("Tiramisu", "Classic Italian dessert with mascarpone and coffee", 17.90m, ProductCategoryEnum.DESSERT);

            var orderItems = new List<CreateOrderItemRequest>
            {
                new CreateOrderItemRequest(product1.Id, 1),
                new CreateOrderItemRequest(product2.Id, 2),
                new CreateOrderItemRequest(product3.Id, 2)
            };
           
            var command = new CreateOrderRequest(customer.Id, orderItems);

            _customerRepositoryMock
                .Setup(x => x.GetCustomerById(customer.Id, new CancellationToken()))
                .ReturnsAsync(customer);

            _productRepositoryMock
                .Setup(x => x.GetProductById(orderItems[0].ProductId, new CancellationToken()))
                .ReturnsAsync(product1);

            _productRepositoryMock
                .Setup(x => x.GetProductById(orderItems[1].ProductId, new CancellationToken()))
                .ReturnsAsync(product2);

            _productRepositoryMock
                .Setup(x => x.GetProductById(orderItems[2].ProductId, new CancellationToken()))
                .ReturnsAsync(product3);

            // Act
            var result = await _service.Execute(command, new CancellationToken());

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.That(_service.OrderCreatedMessage.Equals(result.Message));
            _customerRepositoryMock.Verify(x => x.GetCustomerById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _productRepositoryMock.Verify(x => x.GetProductById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-2)]
        public async Task CreateOrder_ShouldReturn_InvalidItemQuantityError(int quantity)
        {           
            var orderItems = new List<CreateOrderItemRequest>
            {
                new CreateOrderItemRequest(new Guid(), quantity)
            };

            var command = new CreateOrderRequest(new Guid(), orderItems);

            // Act
            var result = await _service.Execute(command, new CancellationToken());

            // Assert
            Assert.IsTrue(!result.IsSuccess);
            Assert.That(_service.InvalidItemQuantityError.Equals(result.Message));
            _customerRepositoryMock.Verify(x => x.GetCustomerById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
            _productRepositoryMock.Verify(x => x.GetProductById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task CreateOrder_ShouldReturn_CustomerErrorMessage()
        {
            var orderItems = new List<CreateOrderItemRequest>
            {
                new CreateOrderItemRequest(new Guid(), 1)
            };

            var command = new CreateOrderRequest(new Guid(), orderItems);
            
            _customerRepositoryMock
                .Setup(x => x.GetCustomerById(new Guid(), new CancellationToken()))
                .ReturnsAsync((Customer?)null);

            // Act
            var result = await _service.Execute(command, new CancellationToken());

            // Assert
            Assert.IsTrue(!result.IsSuccess);
            Assert.That(_service.CustomerErrorMessage.Equals(result.Message));
            _customerRepositoryMock.Verify(x => x.GetCustomerById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _productRepositoryMock.Verify(x => x.GetProductById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
