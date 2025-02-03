using PaymentGateway.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "currency is required.")]
        public string Currency { get; set; }
        public MockPaymentStatusEnum Status { get; set; } // "Pending", "Success", "Failed"
        public string StatusStr { get { return this.Status.ToString(); } } // "Pending", "Success", "Failed"

        [Required(ErrorMessage = "timestamp is required.")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Customer email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [RegularExpression("^(card|bank_transfer)$", ErrorMessage = "Payment method must be either 'card' or 'bank_transfer'.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Card or account details are required.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Card or account details must be between 10 and 100 characters.")]
        public string CardOrAccountDetails { get; set; }
    }




}
