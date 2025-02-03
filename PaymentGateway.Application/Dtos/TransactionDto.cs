using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public Guid TransactionID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; } // "Pending", "Success", "Failed"
        public DateTime Timestamp { get; set; }
        public string CustomerEmail { get; set; }
        public string PaymentMethod { get; set; } // e.g., "card", "bank_transfer"
        public string CardOrAccountDetails { get; set; }
    }
}
