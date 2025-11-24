using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PolicyNotesService.Data;
using PolicyNotesService.Models;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add InMemory DbContext
builder.Services.AddDbContext<PolicyNotesDbContext>(opt =>
    opt.UseInMemoryDatabase("PolicyNotesDb"));

// Register repositories and services
builder.Services.AddScoped<INoteRepository, PolicyNotesRepository>();
builder.Services.AddScoped<IPolicyNotesService, PolicyNotesServiceImpl>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PolicyNotes API",
        Version = "v1"
    });
});

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PolicyNotes API v1");
    c.RoutePrefix = string.Empty; // Optional: serve Swagger UI at root
});

// Minimal API endpoints
app.MapPost("/notes", async (PolicyNote note, IPolicyNotesService svc) =>
{
    var created = await svc.AddNoteAsync(note);
    return Results.Created($"/notes/{created.Id}", created);
});

app.MapGet("/notes", async (IPolicyNotesService svc) =>
{
    var notes = await svc.GetAllNotesAsync();
    return Results.Ok(notes);
});

app.MapGet("/notes/{id}", async (int id, IPolicyNotesService svc) =>
{
    var note = await svc.GetNoteByIdAsync(id);
    return note is not null ? Results.Ok(note) : Results.NotFound();
});

app.Run();
public partial class Program { }
