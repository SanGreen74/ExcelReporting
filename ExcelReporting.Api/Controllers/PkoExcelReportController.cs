using ExcelReporting.Client;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.AspNetCore.Mvc;

namespace ExcelReporting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PkoExcelReportController : ControllerBase
{
    [HttpGet("getsmth")]
    public async Task<IActionResult> Get()
    {
        return NotFound();
    }
    
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