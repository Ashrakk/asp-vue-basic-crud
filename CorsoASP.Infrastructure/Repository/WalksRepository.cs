using Microsoft.EntityFrameworkCore;
using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Models;
using CorsoASP.Core.Views;
using CorsoASP.Infrastructure.Data;
namespace CorsoASP.Infrastructure.Repository;

public class WalksRepository : BaseRepository<Walks>, IWalksRepository
{
    private readonly AppDbContext _context; 
    
    public WalksRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Walks> GetById(int id)
    {
        return await _context.Set<Walks>()
            .Include(w => w.Regions)
            .Include(w => w.Difficulty)
            .AsNoTracking()
            .FirstAsync(w => w.ID == id);
    }
    public async Task<PaginatedDataView<Walks>> GetPaginatedData(int pageNumber, int pageSize, int? lengthMin, int? lengthMax, int? difficultyFK, int? regionFK)
    {
        var query = _context.Set<Walks>()
            .Include(w => w.Regions)
            .Include(w => w.Difficulty)
            .AsQueryable();

        if (difficultyFK.HasValue && difficultyFK > 0)
            query = query.Where(w => w.DifficultyFK == difficultyFK);
        if (regionFK.HasValue && regionFK > 0)
            query = query.Where(w => w.RegionFK == regionFK);
        
        var totalCount = await query.CountAsync();

        var data = await query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedDataView<Walks>(data, totalCount);
    }
}
