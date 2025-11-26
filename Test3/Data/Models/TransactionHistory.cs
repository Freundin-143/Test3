using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test3.Data.Models
{
    public class TransactionHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("FullName")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("UnitNumber")]
        public string UnitNumber { get; set; } = string.Empty;

        [BsonElement("LandlordId")]
        public string LandlordId { get; set; } = string.Empty;

        [BsonElement("PaymentAmount")]
        public decimal PaymentAmount { get; set; }

        [BsonElement("PaymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;

        [BsonElement("PaymentDate")]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [BsonElement("Status")]
        public string Status { get; set; } = "COMPLETED";

        [BsonElement("RoomPrice")]
        public decimal RoomPrice { get; set; }

        [BsonElement("ElectricityBill")]
        public decimal ElectricityBill { get; set; }

        [BsonElement("WaterBill")]
        public decimal WaterBill { get; set; }

        [BsonElement("TotalAmount")]
        public decimal TotalAmount { get; set; }
    }
}