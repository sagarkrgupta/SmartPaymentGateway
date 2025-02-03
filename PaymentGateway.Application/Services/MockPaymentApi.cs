using PaymentGateway.Application.Dtos;
using PaymentGateway.Application.MockAPI;
using PaymentGateway.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services.MockAPI
{
    public class MockPaymentApi : IMockPaymentApi
    {
        private static readonly Random _random = new Random();
        public async Task<PaymentResponseDto> ProcessPaymentAsync(Guid transactionId, PaymentRequestDto request)
        {
            // Simulate delay (e.g., 3 seconds)
            await Task.Delay(3000);

            // Simulate random status (success, pending, failed)            
            // Generate a random integer between 0 and 3 (inclusive)
            var randomValue = Random.Shared.Next(4);

            var status = randomValue switch
            {
                0 => MockPaymentStatusEnum.Success,
                1 => MockPaymentStatusEnum.Pending,
                2 => MockPaymentStatusEnum.Failed,
                _ => MockPaymentStatusEnum.Unknown // Fallback for unexpected values
            };

            return new PaymentResponseDto
            {
                TransactionId = transactionId,
                Status = status
            };
        }

        public int GetRandomValue()
        {
            
            return Random.Shared.Next(4); 
        }
    }
}
