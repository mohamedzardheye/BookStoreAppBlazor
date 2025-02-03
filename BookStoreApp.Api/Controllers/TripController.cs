using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TripController : ControllerBase
    {
        private readonly MongoDBSettings databaseSettings;
        private readonly IMongoCollection<Trip> _tripListCollection;
        private readonly IMapper mapper;

        public TripController(IOptions<MongoDBSettings> databaseOptions,
      IMongoDatabase mongoDatabase, IMapper mapper)
        {
            databaseSettings = databaseOptions.Value;
            _tripListCollection = mongoDatabase.GetCollection<Trip>("Trip");
            this.mapper = mapper;
        }



        [HttpGet]

        public async Task<List<Trip>> Get()
        {
            return await _tripListCollection.Find(new BsonDocument()).SortByDescending(y => y.Id).ToListAsync();

        }


        [HttpGet("{id}")]
        public async Task<Trip> Get(string id)
        {

            return await _tripListCollection.Find(Builders<Trip>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();

        }


        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Trip> filter = Builders<Trip>.Filter.Eq("Id", id);
            await _tripListCollection.DeleteOneAsync(filter);
            return;
        }


     

      
    }

}
