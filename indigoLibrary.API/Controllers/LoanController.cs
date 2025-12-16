using indigoLibrary.Application.DTOs;
using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace indigoLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly LoanService _loanService;


        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanRequestDto request)
        {
            try
            {
                var result = await _loanService.CreateLoanAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLoan(Guid id)
        {
            try
            {
                var result = await _loanService.GetLoanAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}