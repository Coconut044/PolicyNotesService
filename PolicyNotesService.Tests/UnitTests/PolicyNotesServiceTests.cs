using Moq;
using PolicyNotesService.Models;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;
using Xunit; // Make sure Xunit is included

namespace PolicyNotesService.Tests.UnitTests;

[Trait("Category", "Unit")]  // ✅ Add this line
public class PolicyNotesServiceTests
{
    [Fact]
    public async Task AddNote_ShouldAddNote()
    {
        var mock = new Mock<INoteRepository>();
        mock.Setup(r => r.AddAsync(It.IsAny<PolicyNote>()))
            .ReturnsAsync((PolicyNote n) => n);

        var service = new PolicyNotesServiceImpl(mock.Object);

        var note = new PolicyNote { PolicyNumber = "P1", Note = "Test" };
        var result = await service.AddNoteAsync(note);

        Assert.Equal("P1", result.PolicyNumber);
        Assert.Equal("Test", result.Note);
    }

    [Fact]
    public async Task GetAllNotes_ShouldReturnList()
    {
        var mock = new Mock<INoteRepository>();
        mock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<PolicyNote> { new PolicyNote { Id = 1 } });

        var service = new PolicyNotesServiceImpl(mock.Object);

        var result = await service.GetAllNotesAsync();

        Assert.Single(result);
    }
}
