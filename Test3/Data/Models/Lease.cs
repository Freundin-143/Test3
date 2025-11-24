using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test3.Data.Models
{
    public class Lease
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("LandlordId")]
        public string LandlordId { get; set; } = string.Empty;

        [BsonElement("FullName")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("UnitNumber")]
        public string UnitNumber { get; set; } = string.Empty;

        [BsonElement("Contact")]
        public string Contact { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("MoveInDate")]
        public DateTime MoveInDate { get; set; } = DateTime.UtcNow;

        [BsonElement("LeaseDuration")]
        public string LeaseDuration { get; set; } = string.Empty;

        [BsonElement("RentAmount")]
        public decimal RentAmount { get; set; }

        [BsonElement("SecurityDeposit")]
        public decimal SecurityDeposit { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("IsActive")]
        public bool IsActive { get; set; } = true;
    }
}
