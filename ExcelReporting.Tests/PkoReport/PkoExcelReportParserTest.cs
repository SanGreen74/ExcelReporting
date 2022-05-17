using System.IO;
using System.Threading.Tasks;
using ExcelReporting.Api;
using ExcelReporting.Client;
using ExcelReporting.Client.Pko;
using FluentAssertions;
using NUnit.Framework;

namespace ExcelReporting.Tests.PkoReport;

internal static class PkoReportFileHelper
{
    public static byte[] ReadAllBytes(string path)
    {
        return File.ReadAllBytes($"PkoReport\\Data\\{path}");
    }
}

[TestFixture]
public class PkoExcelReportParserTest
{
    private LocalApplication application;
    private readonly ExcelReportClient client = new ExcelReportClient(Constants.LocalUrl);

    [OneTimeSetUp]
    public async Task SetUp()
    {
        application = await LocalApplication.StartAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await application.StopAsync();
    }
    
    [Test]
    public void ParseReport_ShouldReturnCorrectValue()
    {
        var bytes = PkoReportFileHelper.ReadAllBytes("parse_test.xlsx");
        var response = client.Pko.ParseReport(new PkoExcelReportParseRequest
        {
            ExcelContent = bytes
        });

        response.StatusCode.Should().Be(200);
        var result = response.Result;
        result.LastComplicationDate.Should().Be(Date.Parse("04.01.2022"));
        result.LastDocumentNumber.Should().Be(2);
        result.LastAcceptedByPerson.Should().Be("Брагина Е.А");
        result.LastZCauseNumber.Should().Be(230);
        result.AcceptedByPersons.Should().BeEquivalentTo(new[] {"Сатин Н.А.", "Брагина Е.А"},
            options => options.WithStrictOrdering());
    }

    [Test]
    public void CalculateNext_ShouldUpdateValue()
    {
        var content = PkoReportFileHelper.ReadAllBytes("calculate_next_test.xlsx");
        var request = new PkoCalculateNextRequest
        {
            Debit = new Currency
            {
                Kopecks = 37,
                Roubles = 7777
            },
            ComplicationDate = Date.Parse("12.08.1998"),
            DocumentNumber = 1337,
            ExcelContent = content,
            AcceptedByPerson = "Путин В.В.",
            ZCauseNumber = 9911
        };
        var operationResult = client.Pko.CalculateNext(request);
        operationResult.StatusCode.Should().Be(200);
        File.WriteAllBytes("result.xlsx", operationResult.Result.ExcelContent);
        var parseReportResponse = client.Pko.ParseReport(new PkoExcelReportParseRequest
        {
            ExcelContent = operationResult.Result.ExcelContent
        });

        parseReportResponse.StatusCode.Should().Be(200);
        var parseReportResult = parseReportResponse.Result;
        parseReportResult.AcceptedByPersons
            .Should()
            .BeEquivalentTo(new[] {"Сатин Н.А.", "Брагина Е.А", "Путин В.В."}, x => x.WithStrictOrdering());
        parseReportResult.LastComplicationDate.Should().Be(request.ComplicationDate);
        parseReportResult.LastDocumentNumber.Should().Be(request.DocumentNumber);
        parseReportResult.LastAcceptedByPerson.Should().Be(request.AcceptedByPerson);
        parseReportResult.LastZCauseNumber.Should().Be(request.ZCauseNumber);
    }
}