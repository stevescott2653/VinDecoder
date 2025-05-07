using Microsoft.Extensions.Logging;
using VinDecoderSpike;
using VinDecoderSpike.Classes;
using VinDecoderSpike.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<VinValidator>(); // Register VinValidator
builder.Services.AddHttpClient<VinService>(); // Register HttpClient for VinService
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Use a generic error handler for production
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

// Map a global error handler for production
app.Map("/error", () =>
{
    return Results.Problem("An unexpected error occurred. Please try again later.");
});

// Map the VIN endpoint
app.MapGet("/vin/{vin}", async (string vin) =>
{
    var vinService = app.Services.GetRequiredService<VinService>();
    try
    {
        var vinDetails = await vinService.GetVinDetails(vin);
        if (vinDetails == null)
        {
            return Results.NotFound($"No details found for VIN: {vin}");
        }
        return Results.Ok(vinDetails);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while processing the VIN: {Vin}", vin);
        return Results.Problem("An unexpected error occurred. Please try again later.");
    }
})
.WithOpenApi(operation => operation
    .WithSummary("Get details for a specific VIN")
    .WithDescription("Fetches vehicle details for the provided VIN using the NHTSA API.")
    .WithResponse<Root>(200, "VIN details retrieved successfully.")
    .WithResponse<string>(400, "Invalid VIN provided.")
    .WithResponse<string>(404, "No details found for the VIN.")
    .WithResponse<string>(500, "An unexpected error occurred."));

app.Run();
