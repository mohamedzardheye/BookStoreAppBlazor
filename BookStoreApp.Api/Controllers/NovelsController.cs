using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Models.Novel;
using BookStoreApp.Api.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovelsController : ControllerBase
    {
        private readonly MongoDBSettings databaseSettings;
        private readonly IMongoCollection<Novel> _novelListCollection;
        private readonly IMapper mapper;

        public NovelsController(IOptions<MongoDBSettings> databaseOptions,
      IMongoDatabase mongoDatabase, IMapper mapper)
        {
            databaseSettings = databaseOptions.Value;
            _novelListCollection = mongoDatabase.GetCollection<Novel>("Novel");
            this.mapper = mapper;
        }



        [HttpPost]
        public async Task<ActionResult<NovelCreateDto>> Post([FromBody] NovelCreateDto novelCreateDto)
        {
            var novelDto = mapper.Map<Novel>(novelCreateDto);


            await _novelListCollection.InsertOneAsync(novelDto);
            return CreatedAtAction(nameof(Get), new { id = novelDto.Id }, novelDto);
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<NovelReadOnlyDto>>> Get()
        {
            try
            {
                var novals = await _novelListCollection.Find(new BsonDocument()).SortByDescending(y => y.Id).ToListAsync();
                var novalDtos = mapper.Map<IEnumerable<NovelReadOnlyDto>>(novals);
                return Ok(novalDtos);
            }
            catch (Exception ex)
            {
               // logger.LogError(ex, $"Error Performing Get{nameof(Get)}");
                return StatusCode(500, Messages.Error500Message);
            }


        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<NovelReadOnlyDto>>> GetSearchNovels(string? searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;

                FilterDefinition<Novel> filter = FilterDefinition<Novel>.Empty; // Default: No filter, return all

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    filter = Builders<Novel>.Filter.Or(
                        Builders<Novel>.Filter.Regex(n => n.Summary, new BsonRegularExpression($".*{searchTerm}.*", "i")),
                        Builders<Novel>.Filter.Regex(n => n.Title, new BsonRegularExpression($".*{searchTerm}.*", "i"))
                    );
                }

                var novels = await _novelListCollection
                    .Find(filter)
                    .SortByDescending(n => n.Id) // Sorting by ID
                    .Skip((pageNumber - 1) * pageSize) // Skip previous pages
                    .Limit(pageSize) // Limit to page size
                    .ToListAsync();

                var totalCount = await _novelListCollection.CountDocumentsAsync(filter); // Get total items count

                var novelDtos = mapper.Map<IEnumerable<NovelReadOnlyDto>>(novels);

                var response = new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                    Novels = novelDtos
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // logger.LogError(ex, $"Error Performing Get{nameof(GetSearchNovels)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<Novel> Get(string id)
        {

            return await _novelListCollection.Find(Builders<Novel>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();

        }


        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Novel> filter = Builders<Novel>.Filter.Eq("Id", id);
            await _novelListCollection.DeleteOneAsync(filter);
            return;
        }





    }

}