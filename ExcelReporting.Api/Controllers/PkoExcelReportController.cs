using ExcelReporting.Client;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExcelReporting.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PkoExcelReportController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> logger;

    public PkoExcelReportController(ILogger<WeatherForecastController> logger)
    {
        this.logger = logger;
    }

    [HttpPost("parse")]
    public IActionResult Get([FromBody] PkoExcelReportParseRequest request)
    {
        var parser = new PkoExcelReportParser(ExcelReportPackageProvider.Get(request.ExcelContent));
        var response = new PkoExcelReportParseResponse
        {
            LastDocumentNumber = parser.ParseLastDocumentNumber(),
            LastComplicationDate = parser.ParseLastComplicationDate(),
            LastZCauseNumber = parser.ParseLastZCauseNumber(),
            LastAcceptedByPerson = parser.ParseLastAcceptedByPerson(),
            AcceptedByPersons = parser.ParseAcceptedByPersons()
        };

        return Ok(response);
    }
}