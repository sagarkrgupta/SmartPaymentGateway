using PaymentGateway.Application.Dtos;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.MockAPI;
using PaymentGateway.Common.Enums;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Data;
using PaymentGateway.Services;
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
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IMockPaymentApi _mockApi;


        public PaymentService(AppDbContext dbContext, IRabbitMqPublisher rabbitMqPublisher, IMockPaymentApi mockApi)
        {
            _dbContext = dbContext;
            _rabbitMqPublisher = rabbitMqPublisher;
            _mockApi = mockApi;
        }

        public async Task<object> ProcessPayment(PaymentRequestDto request)
        {
            await Task.Delay(3000); // Simulate delay

            var transaction = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                UserID = 1, // Example UserID
                Amount = request.Amount,
                Currency = request.Currency,               
                Timestamp = DateTime.UtcNow,
                PaymentMethod = request.PaymentMethod,
                CardOrAccountDetails = request.CardOrAccountDetails,
                CustomerEmail = request.Email,                
            };


            //TODO: Call the mock payment API
            var apiResponse = await _mockApi.ProcessPaymentAsync(transaction.TransactionID, request);
            transaction.Status = apiResponse!= null? (int)transaction.Status : (int)MockPaymentStatusEnum.Failed;




            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            // Publish "payment initiated" event
            await _rabbitMqPublisher.PublishEvent("payment_initiated", transaction.TransactionID.ToString());


            return new { Status = apiResponse.Status.ToString(), TransactionId = transaction.TransactionID };
        }
    }
}
