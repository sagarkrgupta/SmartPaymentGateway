using PaymentGateway.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<dynamic> ProcessPayment(PaymentRequestDto request);
    }
}
