using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace KLA.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DollarCoversionController : ControllerBase
{
    [HttpPost("/api/v1/convertDollar")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    [ProducesResponseType(statusCode: 500, type: typeof(ProblemDetails))]
    public string ConvertNumberToString([FromBody]string number)
    {
        return number;
    } 
}
        
