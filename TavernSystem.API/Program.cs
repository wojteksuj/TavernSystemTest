using System.Text.Json;
using System.Text.Json.Nodes;
using TavernSystem.Application;
using TavernSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MyDatabase");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Can't connect to database, wrong connection string");
}
builder.Services.AddSingleton<IAdventurerService, AdventurerService>(deviceService => new AdventurerService(connectionString));

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           
builder.Services.AddAuthorization();      

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapGet("/api/adventurers", (IAdventurerService adventurerService) =>
{
    try
    {
        return Results.Ok(adventurerService.GetAllAdventurers());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    
});

app.MapGet("/api/adventurers/{id}", (int id, IAdventurerService service) =>
{
    AdventurerPerson? adventurer = service.GetAdventurerById(id);
    if(adventurer == null) return Results.NotFound();
    return Results.Ok(adventurer);

});

app.MapPost("/api/adventurers", async (IAdventurerService deviceService, HttpRequest request) =>
    {
        
        using var reader = new StreamReader(request.Body);
        string rawJson = await reader.ReadToEndAsync();

        var json = JsonNode.Parse(rawJson);
        if (json is null)
            return Results.BadRequest("Invalid JSON");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return Results.Created();
    })
    .Accepts<string>("application/json");


app.Run();
