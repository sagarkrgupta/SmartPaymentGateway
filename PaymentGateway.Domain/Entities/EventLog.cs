using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Entities
{
    public class EventLog
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public string TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
