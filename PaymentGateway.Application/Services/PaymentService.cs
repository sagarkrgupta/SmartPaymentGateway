using PaymentGateway.Application.Dtos;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _dbContext;
        private readonly RabbitMqPublisher _rabbitMqPublisher;

        public PaymentService(AppDbContext dbContext, RabbitMqPublisher rabbitMqPublisher)
        {
            _dbContext = dbContext;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public async Task<object> ProcessPayment(PaymentRequestDto request)
        {
            await Task.Delay(3000); // Simulate delay
            var random = new Random();
            var status = random.Next(3) switch
            {
                0 => "Success",
                1 => "Pending",
                _ => "Failed"
            };

            var transaction = new Transaction
            {
                UserID = 1, // Example UserID
                Amount = request.Amount,
                Currency = request.Currency,
                Status = status,
                Timestamp = DateTime.UtcNow
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            _rabbitMqPublisher.Publish($"Transaction {transaction.TransactionID} - {status}");

            return new { Status = status, TransactionId = transaction.TransactionID };
        }
    }
}
