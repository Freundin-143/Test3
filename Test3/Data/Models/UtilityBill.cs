using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test3.Data.Models
{
    public class UtilityBill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("RoomId")]
        public string RoomId { get; set; } = string.Empty;

        [BsonElement("UnitNumber")]
        public string UnitNumber { get; set; } = string.Empty;

        [BsonElement("LandlordId")]
        public string LandlordId { get; set; } = string.Empty;

        [BsonElement("ElectricityBill")]
        public decimal ElectricityBill { get; set; }

        [BsonElement("WaterBill")]
        public decimal WaterBill { get; set; }

        [BsonElement("Month")]
        public int Month { get; set; }

        [BsonElement("Year")]
        public int Year { get; set; }

        [BsonElement("IsPaid")]
        public bool IsPaid { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("DueDate")]
        public DateTime DueDate { get; set; }
    }
}