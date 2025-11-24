using MongoDB.Driver;
using Test3.Data.Models;

namespace Test3.Data.Services
{
    public class LeaseService
    {
        private readonly IMongoCollection<Lease> _leases;

        public LeaseService(IMongoDatabase database)
        {
            _leases = database.GetCollection<Lease>("Leases");
        }

        // Get all leases for a landlord
        public async Task<List<Lease>> GetLeasesByLandlordAsync(string landlordId)
        {
            return await _leases.Find(x => x.LandlordId == landlordId && x.IsActive)
                               .SortByDescending(x => x.CreatedAt)
                               .ToListAsync();
        }

        // Create new lease
        public async Task CreateLeaseAsync(Lease lease)
        {
            await _leases.InsertOneAsync(lease);
        }

        // Check if unit is already occupied
        public async Task<bool> IsUnitOccupiedAsync(string landlordId, string unitNumber)
        {
            var existingLease = await _leases.Find(x =>
                x.LandlordId == landlordId &&
                x.UnitNumber == unitNumber &&
                x.IsActive
            ).FirstOrDefaultAsync();

            return existingLease != null;
        }

        // Get lease by ID
        public async Task<Lease?> GetLeaseByIdAsync(string id)
        {
            return await _leases.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // Update lease
        public async Task UpdateLeaseAsync(string id, Lease lease)
        {
            await _leases.ReplaceOneAsync(x => x.Id == id, lease);
        }

        // Delete lease (soft delete)
        public async Task DeleteLeaseAsync(string id)
        {
            var update = Builders<Lease>.Update.Set(x => x.IsActive, false);
            await _leases.UpdateOneAsync(x => x.Id == id, update);
        }
    }
}
