using MongoDB.Driver;
using Test3.Data.Models;

namespace Test3.Data.Services
{
    public class UtilityBillService
    {
        private readonly IMongoCollection<UtilityBill> _utilityBills;

        public UtilityBillService(IMongoDatabase database)
        {
            _utilityBills = database.GetCollection<UtilityBill>("UtilityBills");
        }

        public async Task<UtilityBill?> GetCurrentBillAsync(string roomId)
        {
            var currentDate = DateTime.UtcNow;
            return await _utilityBills.Find(x =>
                x.RoomId == roomId &&
                x.Month == currentDate.Month &&
                x.Year == currentDate.Year
            ).FirstOrDefaultAsync();
        }

        public async Task CreateOrUpdateBillAsync(UtilityBill bill)
        {
            var existingBill = await GetCurrentBillAsync(bill.RoomId);

            if (existingBill != null)
            {
                bill.Id = existingBill.Id;
                await _utilityBills.ReplaceOneAsync(x => x.Id == bill.Id, bill);
            }
            else
            {
                await _utilityBills.InsertOneAsync(bill);
            }
        }

        public async Task<List<UtilityBill>> GetBillsByLandlordAsync(string landlordId)
        {
            return await _utilityBills.Find(x => x.LandlordId == landlordId)
                                     .SortByDescending(x => x.CreatedAt)
                                     .ToListAsync();
        }

        public async Task<UtilityBill?> GetBillByUnitNumberAsync(string unitNumber, int month, int year)
        {
            return await _utilityBills.Find(x =>
                x.UnitNumber == unitNumber &&
                x.Month == month &&
                x.Year == year
            ).FirstOrDefaultAsync();
        }
        // Add this method to UtilityBillService.cs
        public async Task MarkBillAsPaidAsync(string billId)
        {
            var filter = Builders<UtilityBill>.Filter.Eq(x => x.Id, billId);
            var update = Builders<UtilityBill>.Update
                .Set(x => x.IsPaid, true);

            await _utilityBills.UpdateOneAsync(filter, update);
        }

        public async Task DeleteBillAsync(string billId)
        {
            await _utilityBills.DeleteOneAsync(x => x.Id == billId);
        }
    }
}