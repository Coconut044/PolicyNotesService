using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Models;

namespace PolicyNotesService.Repositories;

public class PolicyNotesRepository : INoteRepository
{
    private readonly PolicyNotesDbContext _db;

    public PolicyNotesRepository(PolicyNotesDbContext db)
    {
        _db = db;
    }

    public async Task<PolicyNote> AddAsync(PolicyNote note)
    {
        _db.PolicyNotes.Add(note);
        await _db.SaveChangesAsync();
        return note;
    }

    public async Task<IEnumerable<PolicyNote>> GetAllAsync()
    {
        return await _db.PolicyNotes.ToListAsync();
    }

    public async Task<PolicyNote?> GetByIdAsync(int id)
    {
        return await _db.PolicyNotes.FindAsync(id);
    }
}
