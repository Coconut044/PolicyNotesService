using PolicyNotesService.Models;
using PolicyNotesService.Repositories;

namespace PolicyNotesService.Services;

public class PolicyNotesServiceImpl : IPolicyNotesService
{
    private readonly INoteRepository _repo;

    // ✅ Constructor must match class name
    public PolicyNotesServiceImpl(INoteRepository repo)
    {
        _repo = repo;
    }

    public Task<PolicyNote> AddNoteAsync(PolicyNote note)
        => _repo.AddAsync(note);

    public Task<IEnumerable<PolicyNote>> GetAllNotesAsync()
        => _repo.GetAllAsync();

    public Task<PolicyNote?> GetNoteByIdAsync(int id)
        => _repo.GetByIdAsync(id);
}

