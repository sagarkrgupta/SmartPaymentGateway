using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Application.Dtos;
using PaymentGateway.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Test.Controllers
{
    public class PaymentControllerTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly PaymentController _controller;

        public PaymentControllerTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _controller = new PaymentController(_mockPaymentService.Object);
        }

        [Fact]
        public async Task ProcessPayment_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new PaymentRequestDto
            {
                Amount = 100,
                Currency = "USD",
                CardNumber = "4111111111111111",
                CustomerName = "John Doe",
                Email = "john.doe@example.com",
                PaymentMethod = "card",
                CardOrAccountDetails = "Visa",
            };

            var expectedResult = new { Status = "Success", TransactionId = 12345 };
            _mockPaymentService.Setup(service => service.ProcessPayment(It.IsAny<PaymentRequestDto>()))
                               .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.ProcessPayment(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value as dynamic;
            Assert.Equal("Success", returnValue.Status);
            Assert.Equal(12345, returnValue.TransactionId);
        }


        [Fact]
        public async Task ProcessPayment_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new PaymentRequestDto(); // Invalid request with missing fields

            _mockPaymentService.Setup(service => service.ProcessPayment(It.IsAny<PaymentRequestDto>()))
                               .ThrowsAsync(new ArgumentException("Invalid payment request"));

            // Act
            var result = await _controller.ProcessPayment(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid payment request", badRequestResult.Value);
        }

    }


}
