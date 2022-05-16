using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace ExcelReporting.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5003");
                });
    }
}