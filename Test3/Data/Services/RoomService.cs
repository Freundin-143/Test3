using MongoDB.Driver;
using Test3.Data.Models;

namespace Test3.Data.Services
{
    public class RoomService
    {
        private readonly IMongoCollection<Room> _rooms;

        public RoomService(IMongoDatabase database)
        {
            _rooms = database.GetCollection<Room>("Rooms");
        }

        // Get rooms by apartment ID
        public async Task<List<Room>> GetRoomsByApartmentIdAsync(string apartmentId)
        {
            return await _rooms.Find(x => x.ApartmentId == apartmentId).ToListAsync();
        }

        // Create new room
        public async Task<Room> CreateRoomAsync(string apartmentId)
        {
            // Get existing rooms to determine next unit number
            var existingRooms = await GetRoomsByApartmentIdAsync(apartmentId);
            var nextUnitNumber = (existingRooms.Count + 1).ToString();

            var room = new Room
            {
                ApartmentId = apartmentId,
                UnitNumber = $"Unit {nextUnitNumber}",
                RoomType = "Single",
                RoomPrice = 0,
                BedCount = 1,
                Aircon = false,
                Status = "Available",
                Description = ""
            };

            await _rooms.InsertOneAsync(room);
            return room;
        }

        // Update room
        public async Task UpdateRoomAsync(string id, Room room)
        {
            await _rooms.ReplaceOneAsync(x => x.Id == id, room);
        }

        // Delete room
        public async Task DeleteRoomAsync(string id)
        {
            await _rooms.DeleteOneAsync(x => x.Id == id);
        }

        // Get room by ID
        public async Task<Room?> GetRoomByIdAsync(string id)
        {
            return await _rooms.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // Get room statistics for an apartment
        public async Task<(int totalRooms, int availableRooms)> GetRoomStatsAsync(string apartmentId)
        {
            var rooms = await GetRoomsByApartmentIdAsync(apartmentId);
            var totalRooms = rooms.Count;
            var availableRooms = rooms.Count(r => r.Status == "Available");

            return (totalRooms, availableRooms);
        }

        public async Task<List<Room>> GetRoomsByLandlordIdAsync(string landlordId)
        {
            // First get all apartments for this landlord
            var apartmentsCollection = _rooms.Database.GetCollection<Apartment>("Apartment");
            var landlordApartments = await apartmentsCollection
                .Find(x => x.LandlordId == landlordId)
                .Project(x => x.Id)
                .ToListAsync();

            // Then get all rooms for these apartments
            return await _rooms.Find(x => landlordApartments.Contains(x.ApartmentId)).ToListAsync();
        }
    }
}