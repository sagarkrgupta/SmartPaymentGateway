using PaymentGateway.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Dtos
{
    public class PaymentResponseDto
    {
        public Guid TransactionId { get; set; }
        public MockPaymentStatusEnum Status { get; set; }
    }
}
