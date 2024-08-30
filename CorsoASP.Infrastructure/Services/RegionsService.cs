using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Mapping;
using CorsoASP.Core.Views;

namespace CorsoASP.Infrastructure.Services;

public class RegionsService: IRegionsService
{
    private readonly IRegionsRepository _regionsRepository;

    public RegionsService(IRegionsRepository regionsRepo)
    {
        _regionsRepository = regionsRepo;
    }
    
    public async Task<RegionsView> GetRegion(int id)
    {
        var result = await _regionsRepository.GetById(id);
        if (result != null)
            return RegionsMap.ModelToView(result);
        else
            return null;
    }

    public async Task<IEnumerable<RegionsView>> GetRegions()
    {
        return RegionsMap.ModelToView(await _regionsRepository.GetAll());
    }
}