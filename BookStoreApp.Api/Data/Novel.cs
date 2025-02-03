using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookStoreApp.Api.Data
{
    public class Novel
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? Title { get; set; }

        public int? Year { get; set; }

        public string? Isbn { get; set; }

        public string? Summary { get; set; }

        public string? Image { get; set; }

        public decimal? Price { get; set; }

        public int? AuthorId { get; set; }

    }
}
