using CorsoASP.Core.Interfaces;
using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Models;
using CorsoASP.Infrastructure.Data;

namespace CorsoASP.Infrastructure.Repository;

public class DifficultyRepository : BaseRepository<Difficulty>, IDifficultyRepository
{
    public DifficultyRepository(AppDbContext context) : base(context)
    {
    }
}