using PolicyNotesService.Models;

namespace PolicyNotesService.Repositories;

public interface INoteRepository
{
    Task<PolicyNote> AddAsync(PolicyNote note);
    Task<IEnumerable<PolicyNote>> GetAllAsync();
    Task<PolicyNote?> GetByIdAsync(int id);
}
