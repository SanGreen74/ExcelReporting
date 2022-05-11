using ExcelReporting.Client;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.AspNetCore.Mvc;

namespace ExcelReporting.API.Controllers;

[ApiController]
[Route("pko")]
public class PkoExcelReportController : ControllerBase
{
    [HttpPost("parse")]
    public IActionResult ParseReport([FromBody] PkoExcelReportParseRequest request)
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