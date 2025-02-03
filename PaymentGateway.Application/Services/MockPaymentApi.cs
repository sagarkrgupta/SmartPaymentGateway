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
        public async Task<PaymentResponseDto> ProcessPaymentAsync(Guid transactionId, PaymentRequestDto request)
        {
            // Simulate delay (e.g., 3 seconds)
            await Task.Delay(3000);

            // Simulate random status (success, pending, failed)            
            var random = new Random();
            var status = random.Next(3) switch
            {
                0 => MockPaymentStatusEnum.Success,
                1 => MockPaymentStatusEnum.Pending,
                _ => MockPaymentStatusEnum.Failed
            };

            return new PaymentResponseDto
            {
                TransactionId = transactionId,
                Status = status
            };
        }
    }
}
