﻿using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Dtos;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services 
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _dbContext;
        public TransactionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions(DateTime? startDate, DateTime? endDate, string status, int? userId)
        {
            var query = _dbContext.Transactions.AsQueryable();

            if (startDate.HasValue) query = query.Where(t => t.Timestamp >= startDate.Value);
            if (endDate.HasValue) query = query.Where(t => t.Timestamp <= endDate.Value);
            if (!string.IsNullOrEmpty(status)) query = query.Where(t => t.Status == status);
            if (userId.HasValue) query = query.Where(t => t.UserID == userId.Value);

            var transactions = await query.ToListAsync();
            return transactions.Select(t => new TransactionDto
            {
                TransactionID = t.TransactionID,
                UserID = t.UserID,
                Amount = t.Amount,
                Currency = t.Currency,
                Status = t.Status,
                Timestamp = t.Timestamp
            });
        }
    }
}
