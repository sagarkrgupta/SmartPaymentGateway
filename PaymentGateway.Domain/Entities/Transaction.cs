using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; } // "Pending", "Success", "Failed"
        public DateTime Timestamp { get; set; }
    }
}
