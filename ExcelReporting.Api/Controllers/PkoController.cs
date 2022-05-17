using ExcelReporting.Api.Features.Pko.CalculateNext;
using ExcelReporting.Client.Pko;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExcelReporting.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PkoController : ControllerBase
{
    private readonly ILogger<PkoController> logger;
    private readonly PkoNextWorksheetCalculator nextWorksheetCalculator;

    public PkoController(
        ILogger<PkoController> logger,
        PkoNextWorksheetCalculator nextWorksheetCalculator)
    {
        this.logger = logger;
        this.nextWorksheetCalculator = nextWorksheetCalculator;
    }

    [HttpPost("parse")]
    public IActionResult Parse([FromBody] PkoExcelReportParseRequest request)
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

    [HttpPost("calculateNext")]
    public IActionResult CalculateNext([FromBody] PkoCalculateNextRequest request)
    {
        var response = nextWorksheetCalculator.CalculateNext(request);
        return Ok(response);
    }    
}