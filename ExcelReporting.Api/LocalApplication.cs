using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ExcelReporting.Api;

public class LocalApplication
{
    private readonly IHost host;

    public LocalApplication(IHost host)
    {
        this.host = host;
    }
    
    public static async Task<LocalApplication> StartAsync(string[]? args = null)
    {
        args ??= Array.Empty<string>();
        var build = Program.CreateHostBuilder(args).Build();
        await build.StartAsync();
        
        return new LocalApplication(build);
    }

    public async Task StopAsync()
    {
        await host.StopAsync();
    }
}