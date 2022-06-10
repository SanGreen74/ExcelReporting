using System;
using ExcelReporting.Api.Features.Pko.CalculateNext;
using ExcelReporting.Api.Features.Pko.Parse;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExcelReporting.Api.Controllers;

[ApiController]
[Route("api/excelReporting/[controller]")]
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
        var bytes = Convert.FromBase64String(request.ExcelContentBase64);
        var parser = new PkoExcelReportParser(ExcelReportPackageProvider.Get(bytes));
        var response = new PkoExcelReportParseResponse
        {
            LastDocumentNumber = parser.ParseLastDocumentNumber(),
            LastComplicationDate = parser.ParseLastComplicationDate(),
            LastZCauseNumber = parser.ParseLastZCauseNumber(),
            LastAcceptedByPerson = parser.ParseLastAcceptedByPerson(),
            AcceptedByPersons = parser.ParseAcceptedByPersons(),
            ShopAddress = parser.ParseShopAddress()
        };

        return Ok(response);
    }
    

    [HttpPost("parseNext")]
    public IActionResult ParseNext([FromBody] PkoExcelReportParseRequest request)
    {
        var bytes = Convert.FromBase64String(request.ExcelContentBase64);
        var parser = new PkoExcelReportParser(ExcelReportPackageProvider.Get(bytes));
        var response = new PkoExcelReportParseResponse
        {
            LastDocumentNumber = parser.ParseLastDocumentNumber() + 1,
            LastComplicationDate = new Date(parser.ParseLastComplicationDate().ToDateTime().AddDays(1)),
            LastZCauseNumber = parser.ParseLastZCauseNumber() + 1,
            LastAcceptedByPerson = parser.ParseLastAcceptedByPerson(),
            AcceptedByPersons = parser.ParseAcceptedByPersons(),
            ShopAddress = parser.ParseShopAddress()
        };

        return Ok(response);
    }

    [HttpPost("calculateNext")]
    public IActionResult CalculateNext([FromBody] PkoCalculateNextRequest request)
    {
        var response = nextWorksheetCalculator.CalculateNext(request);
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        
        return new FileContentResult(response.Content, contentType)
        {
            FileDownloadName = ".xlsx"
        };
    }    
}