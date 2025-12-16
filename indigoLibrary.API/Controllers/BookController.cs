using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace indigoLibrary.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController(BookService BookService) : ControllerBase
    {
        private readonly BookService _BookService = BookService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            try
            {
                var result = await _BookService.CreateAsync(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}