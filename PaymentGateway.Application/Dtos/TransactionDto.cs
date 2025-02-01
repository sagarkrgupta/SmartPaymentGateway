using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Dtos
{
    public class TransactionDto
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
