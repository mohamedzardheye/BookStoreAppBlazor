using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;
using AutoMapper;
using BookStoreApp.Api.Static;
using Microsoft.AspNetCore.Authorization;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Helpers;
using AutoMapper.QueryableExtensions;


namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    //[AllowAnonymous]
   
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }
        //  logger.LogInformation($"Request to {nameof(GetAuthors)}");
        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
         
            try
            {
                var authors = await _context.Authors.OrderByDescending(x=>x.Id).ToListAsync();
                var authorDtos = mapper.Map<IEnumerable<AuthorReadOnlyDto>>(authors);
                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Get{nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
         
        }

        //public async Task<List<AuthorReadOnlyDto>> SearchAuthor(AuthorFilterDto search)
        //{
        //    try
        //    {
        //        var queryable = _context.Authors.ProjectTo<AuthorReadOnlyDto>(mapper.ConfigurationProvider).AsQueryable();




        //        if (search.Name != null)
        //        {
        //            queryable = queryable.Where(x => x.FirstName.Contains(search.Name));

        //        }
        //        await HttpContext.InsertPaginationParameterInResponse(queryable, search.QuantityPerPage);


        //        var authors = await queryable.Paginate(search).ToListAsync();


        //        return authors;




        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, $"Error Performing Get{nameof(GetAuthors)}");
        //        return null;
        //    }
        //}

        [HttpGet("search")]
        public async Task<ActionResult<PaginatedResponseDto<AuthorReadOnlyDto>>> Get([FromQuery] AuthorFilterDto pagination)
        {
            try
            {
                var queryable = _context.Authors.AsQueryable();

                if (pagination.FirstName != null)
                {
                    queryable = queryable.Where(x => x.FirstName.Contains(pagination.FirstName));
                }
                if (pagination.LastName != null)
                {
                    queryable = queryable.Where(x => x.LastName.Contains(pagination.LastName));
                }

                // Get the total count of authors that match the filter criteria
                var totalCount = await queryable.CountAsync();

                queryable = queryable.OrderByDescending(x => x.Id);

                // Pagination logic
                var authors = await queryable.Paginate(pagination).ToListAsync();
                var authorDto = mapper.Map<IEnumerable<AuthorReadOnlyDto>>(authors);

                // Wrap the paginated data in a response DTO
                var response = new PaginatedResponseDto<AuthorReadOnlyDto>
                {
                    TotalRecords = totalCount,
                    CurrentPage = pagination.Page, // Assuming pagination contains current page info
                    QuantityPerPage = pagination.QuantityPerPage,
                    Data = authorDto.ToList()
                   
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Get{nameof(Get)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {

                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Record Not Found : {nameof(GetAuthor)} - Id: {id}");
                    return NotFound();
                }
                var authorDto = mapper.Map<AuthorReadOnlyDto>(author);

                Console.WriteLine(authorDto.ToString());

                return authorDto;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Get{nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }

           
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorCreateDto authorDto)
        {
            if (id != authorDto.Id)
            {
                logger.LogWarning($"Update Id invalid in: {nameof(PutAuthor)} - Id: {id}");

                return BadRequest();
            }

            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                logger.LogWarning($"Record Not Found : {nameof(PutAuthor)} - Id: {id}");

                return NotFound();
            }

              mapper.Map(authorDto, author);
           

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError($"Error Performing {nameof(PutAuthor)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {

            //var author = new Author
            //{
            //    Bio = authorDto.Bio,
            //    FirstName = authorDto.FirstName,
            //    LastName = authorDto.LastName,
            //};

            var author = mapper.Map<Author>(authorDto);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    logger.LogWarning($"Record Not Found : {nameof(DeleteAuthor)} - Id: {id}");

                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Get{nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }


        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
