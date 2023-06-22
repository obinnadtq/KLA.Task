using Microsoft.AspNetCore.Mvc;

namespace KLA.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DollarCoversionController : ControllerBase
{
    [HttpPost("/api/v1/convertDollar")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    [ProducesResponseType(statusCode: 400, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: 500, type: typeof(ProblemDetails))]
    public IActionResult ConvertNumberToString([FromBody]string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return BadRequest("No valid input entered");
        }

        var splittedString = input.Split(",");

        var (dollarValue, centValue) = ValidateCurrency(splittedString);

        if (dollarValue is null && centValue is null)
        {
            return BadRequest("The values entered are invalid currency values and cannot be converted to words");
        }

        return Ok("Result is okay");
    }

    private static (int?, int?) ValidateCurrency(string[] input)
    {
        if (input.Length == 2)
        {
            return ValidateDollarsAndCents(input);
        }

        else if(input.Length == 1)
        {

            return (ValidateOnlyDollars(input[0]), null);
        }

        return (null, null);

    }

    private static (int?, int?) ValidateDollarsAndCents(string[] input)
    {
        if (!int.TryParse(input[0], out int dollarValue) || !int.TryParse(input[1], out int centValue))
        {
            return (null, null);
        }

        if (dollarValue < 0 || dollarValue > 999999999 || centValue < 0 || centValue > 99)
        {
            return (null, null);
        }

        return (dollarValue, centValue);
    }
    
    private static int? ValidateOnlyDollars(string input)
    {
        if (!int.TryParse(input, out int dollarValue))
        {
            return null;
        }

        if (dollarValue < 0 || dollarValue > 999999999)
        {
            return null;
        }

        return dollarValue;
    }
}
        
