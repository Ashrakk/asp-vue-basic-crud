using CorsoASP.Core.Interfaces;
using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Models;
using CorsoASP.Infrastructure.Data;

namespace CorsoASP.Infrastructure.Repository;

public class RegionsRepository : BaseRepository<Regions>, IRegionsRepository
{
    public RegionsRepository(AppDbContext context) : base(context)
    {
    }
}