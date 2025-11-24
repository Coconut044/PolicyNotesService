using PolicyNotesService.Models;

namespace PolicyNotesService.Services;

public interface IPolicyNotesService
{
    Task<PolicyNote> AddNoteAsync(PolicyNote note);
    Task<IEnumerable<PolicyNote>> GetAllNotesAsync();
    Task<PolicyNote?> GetNoteByIdAsync(int id);
}
