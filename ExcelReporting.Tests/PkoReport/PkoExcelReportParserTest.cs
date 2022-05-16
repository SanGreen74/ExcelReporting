using System.IO;
using System.Threading.Tasks;
using ExcelReporting.Api;
using ExcelReporting.Client;
using NUnit.Framework;

namespace ExcelReporting.Tests.PkoReport;

[TestFixture]
public class PkoExcelReportParserTest
{
    private LocalApplication application;
    
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
    public void METHOD()
    {
        var bytes = File.ReadAllBytes("PkoReport\\Data\\pko.xlsx");
        var pkoExcelReportClient = new PkoExcelReportClient("http://localhost:5003");
        var res = pkoExcelReportClient.ParseReport(new PkoExcelReportParseRequest
        {
            ExcelContent = bytes
        });
    }
}