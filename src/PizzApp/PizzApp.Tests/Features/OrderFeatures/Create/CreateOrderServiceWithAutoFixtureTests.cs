using AutoFixture;
using Moq;
using PizzApp.Application.Features.OrderFeatures.Create;
using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;

namespace PizzApp.Application.Tests.Features.OrderFeatures.Create
{
    [TestFixture]
    public class CreateOrderServiceWithAutoFixtureTests
    {
        protected Mock<IOrderRepository> _orderRepositoryMock = null!;
        protected Mock<ICustomerRepository> _customerRepositoryMock = null!;
        protected Mock<IProductRepository> _productRepositoryMock = null!;
        protected Mock<IUnitOfWork> _unitOfWorkMock = null!;
        protected CreateOrderService _service = null!;
        protected Fixture _fixture = null!;

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

            _fixture = new Fixture();
        }

        [Test]
        public async Task CreateOrder_ShouldReturn_Success()
        {
            // Arrange
            var customer = _fixture.Build<Customer>().Without(x => x.Orders).Create();
                
            var product1 = _fixture.Create<Product>();
            var product2 = _fixture.Create<Product>();
            var product3 = _fixture.Create<Product>();
                
            var orderItems = new List<CreateOrderItemRequest>
            {
                _fixture.Build<CreateOrderItemRequest>().With(x => x.ProductId, product1.Id).Create(),
                _fixture.Build<CreateOrderItemRequest>().With(x => x.ProductId, product2.Id).Create(),
                _fixture.Build<CreateOrderItemRequest>().With(x => x.ProductId, product3.Id).Create(),
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
