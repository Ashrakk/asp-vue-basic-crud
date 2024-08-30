using CorsoASP.Core.Views;

namespace CorsoASP.Core.Interfaces.IService;

public interface IRegionsService
{
    Task<RegionsView> GetRegion(int id);
    Task<IEnumerable<RegionsView>> GetRegions();
}