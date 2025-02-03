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
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> _mockTransactionService;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _mockTransactionService = new Mock<ITransactionService>();
            _controller = new TransactionController(_mockTransactionService.Object);
        }

        [Fact]
        public async Task GetTransactions_ValidQuery_ReturnsOkResult()
        {
            // Arrange
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);
            var status = 1; // Success
            var userId = 123;

            var expectedTransactions = new List<TransactionDto>
            {
                new TransactionDto { TransactionID = Guid.NewGuid(), UserID = 123, Amount = 100, Currency = "USD", Status =  Common.Enums.MockPaymentStatusEnum.Success },
                new TransactionDto { TransactionID = Guid.NewGuid(), UserID = 123, Amount = 200, Currency = "USD", Status =  Common.Enums.MockPaymentStatusEnum.Success }
            };

            _mockTransactionService.Setup(service => service.GetTransactions(startDate, endDate, status, userId))
                                   .ReturnsAsync(expectedTransactions);

            // Act
            var result = await _controller.GetTransactions(startDate, endDate, status, userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TransactionDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetTransactions_NoTransactionsFound_ReturnsEmptyList()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var status = 1; // Success
            var userId = 123;

            _mockTransactionService.Setup(service => service.GetTransactions(startDate, endDate, status, userId))
                                   .ReturnsAsync(new List<TransactionDto>());

            // Act
            var result = await _controller.GetTransactions(startDate, endDate, status, userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TransactionDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

    }
}
