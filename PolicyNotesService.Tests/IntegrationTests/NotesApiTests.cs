using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PolicyNotesService.Models;
using Xunit; // Make sure Xunit is included

namespace PolicyNotesService.Tests.IntegrationTests;

[Trait("Category", "Integration")]  // ✅ Add this line
public class NotesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public NotesApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostNotes_ReturnsCreated()
    {
        var note = new PolicyNote { PolicyNumber = "P100", Note = "Created" };
        var response = await _client.PostAsJsonAsync("/notes", note);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetNotes_ReturnsOk()
    {
        var response = await _client.GetAsync("/notes");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNoteById_Found()
    {
        var note = new PolicyNote { PolicyNumber = "P101", Note = "Find me" };
        var created = await _client.PostAsJsonAsync("/notes", note);
        var createdNote = await created.Content.ReadFromJsonAsync<PolicyNote>();
        var response = await _client.GetAsync($"/notes/{createdNote!.Id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNoteById_NotFound()
    {
        var response = await _client.GetAsync("/notes/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
