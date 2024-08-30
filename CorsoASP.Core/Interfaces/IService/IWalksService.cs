using CorsoASP.Core.Views;

namespace CorsoASP.Core.Interfaces.IService;

//Service -> Handles communication between Repository and Controller
//Converts data to DTO / Mapping 
public interface IWalksService
{
    Task<WalksDetailView?> GetWalk(int id);
    Task<IEnumerable<WalksView>> GetWalks();
    
    Task<PaginatedDataView<WalksDetailView>> GetPaginatedWalks(int pageNumber, int pageSize, int? lengthMin, int? lengthMax, int? difficultyFK, int? regionFK);
    
    Task<bool> ExistsById(int id);
    Task<WalksView> Create(WalksView model);
    Task<bool> Update(WalksView model);
    Task<bool> Delete(int id);
}