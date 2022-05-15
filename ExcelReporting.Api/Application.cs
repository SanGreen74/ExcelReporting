/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();*/

using ExcelReporting.Client;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

new Application().StartAsync().GetAwaiter().GetResult();
Console.ReadLine();

public class Application
{
    private WebApplication? app;

    public async Task StartAsync()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.Converters.Add(new DateConverter());
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        app = builder
            .Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapPost("/test", () => "message_post");
        app.MapGet("/message", () => "message");
        app.MapPost("/pko/parse", ([FromBody] PkoExcelReportParseRequest request) =>
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
            return Results.Ok(response);
        });
        app.Urls.Add("http://localhost:55020/");

        await app.StartAsync();
    }
}