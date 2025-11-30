using MongoDB.Driver;
using Test3.Data.Models;

using System;

namespace Test3.Data.Services
{
    public class LeaseService
    {
        private readonly IMongoCollection<Lease> _leases;

        // Calculate move-out date based on lease duration
        private DateTime? CalculateMoveOutDate(DateTime moveInDate, string leaseDuration)
        {
            return leaseDuration.ToLower() switch
            {
                "6 months" => moveInDate.AddMonths(6),
                "1 year" => moveInDate.AddYears(1),
                "2 years" => moveInDate.AddYears(2),
                "month-to-month" => null, // Active tenants
                _ => null
            };
        }

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
            // Calculate and set move-out date
            lease.MoveOutDate = CalculateMoveOutDate(lease.MoveInDate, lease.LeaseDuration);

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

        // Add this method to LeaseService.cs
        public async Task<List<Lease>> GetAllLeasesAsync()
        {
            return await _leases.Find(_ => true).ToListAsync();
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
