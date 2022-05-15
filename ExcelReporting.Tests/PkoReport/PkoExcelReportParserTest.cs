using System.IO;
using System.Threading.Tasks;
using ExcelReporting.API;
using ExcelReporting.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace ExcelReporting.Tests.PkoReport;

[TestFixture]
public class PkoExcelReportParserTest
{
    private Application app;
    
    [OneTimeSetUp]
    public async Task SetUp()
    {
        app = new Application();
        await app.StartAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
    }

    
    [Test]
    public void METHOD()
    {
        var bytes = File.ReadAllBytes("PkoReport\\Data\\pko.xlsx");
        var pkoExcelReportClient = new PkoExcelReportClient("http://localhost:55020");
        var res = pkoExcelReportClient.ParseReport(new PkoExcelReportParseRequest
        {
            ExcelContent = bytes
        });
    }
}