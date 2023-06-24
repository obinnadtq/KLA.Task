using KLA.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KLA.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DollarCoversionController : ControllerBase
{
    private readonly ICurrencyConverterService currencyConverterService;
    public DollarCoversionController(ICurrencyConverterService currencyConverterService)
    {
        this.currencyConverterService = currencyConverterService;
    }

    [HttpPost("/api/v1/convertDollar")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    [ProducesResponseType(statusCode: 400, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: 500, type: typeof(ProblemDetails))]
    public IActionResult ConvertNumberToString([FromBody]string input)
    {
        try
        {
            var result = currencyConverterService.ConvertDollarToWords(input);

            return Ok(result);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    
}
        
