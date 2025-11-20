using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test3.Data.Models
{
    public class RoomImage
    {
        [BsonElement("ImageData")]
        public byte[] ImageData { get; set; } = Array.Empty<byte>();

        [BsonElement("ImageContentType")]
        public string ImageContentType { get; set; } = string.Empty;

        [BsonElement("ImageFileName")]
        public string ImageFileName { get; set; } = string.Empty;

        [BsonElement("UploadedAt")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }

    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("ApartmentId")]
        public string ApartmentId { get; set; } = string.Empty;

        [BsonElement("UnitNumber")]
        public string UnitNumber { get; set; } = string.Empty;

        [BsonElement("RoomType")]
        public string RoomType { get; set; } = string.Empty;

        [BsonElement("RoomPrice")]
        public decimal RoomPrice { get; set; }

        [BsonElement("BedCount")]
        public int BedCount { get; set; } = 1;

        [BsonElement("Aircon")]
        public bool Aircon { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; } = "Available";

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        // Updated: Support multiple images
        [BsonElement("Images")]
        public List<RoomImage> Images { get; set; } = new();

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}