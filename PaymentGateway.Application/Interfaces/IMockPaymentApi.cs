using PaymentGateway.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.MockAPI
{
    public interface IMockPaymentApi
    {
        Task<PaymentResponseDto> ProcessPaymentAsync(Guid transactionId, PaymentRequestDto request);
    }
}
