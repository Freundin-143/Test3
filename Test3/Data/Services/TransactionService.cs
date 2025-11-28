using MongoDB.Driver;
using Test3.Data.Models;

namespace Test3.Data.Services
{
    public class TransactionService
    {
        private readonly IMongoCollection<TransactionHistory> _transactions;

        public TransactionService(IMongoDatabase database)
        {
            _transactions = database.GetCollection<TransactionHistory>("TransactionHistory");
        }

        public async Task CreateTransactionAsync(TransactionHistory transaction)
        {
            await _transactions.InsertOneAsync(transaction);
        }

        public async Task<List<TransactionHistory>> GetTransactionsByLandlordAsync(string landlordId)
        {
            return await _transactions.Find(x => x.LandlordId == landlordId)
                                    .SortByDescending(x => x.PaymentDate)
                                    .ToListAsync();
        }

        public async Task<List<TransactionHistory>> GetTransactionsByUserAsync(string userId)
        {
            return await _transactions.Find(x => x.UserId == userId)
                                    .SortByDescending(x => x.PaymentDate)
                                    .ToListAsync();
        }

        public async Task<TransactionHistory?> GetLatestTransactionAsync(string userId)
        {
            return await _transactions.Find(x => x.UserId == userId)
                                    .SortByDescending(x => x.PaymentDate)
                                    .FirstOrDefaultAsync();
        }
        // Add this method to TransactionService.cs
        public async Task<bool> HasPaidForMonthAsync(string userId, int month, int year)
        {
            var transaction = await _transactions.Find(x =>
                x.UserId == userId &&
                x.PaymentDate.Month == month &&
                x.PaymentDate.Year == year
            ).FirstOrDefaultAsync();

            return transaction != null;
        }
    }
}