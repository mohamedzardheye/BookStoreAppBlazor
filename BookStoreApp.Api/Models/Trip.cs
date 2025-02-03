
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStoreApp.Api.Models
{
    public class Trip 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null;
        public string userName { get; set; } = null;
        public int userPhone { get; set; } = 0;
        public string providerName { get; set; } = null;
        public int providerPhone { get; set; } = 0;
        public string tripType { get; set; } = null;
        public string tripStatus { get; set; } = null;

        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
