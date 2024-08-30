using CorsoASP.Core.Models;
using CorsoASP.Core.Views;

namespace CorsoASP.Core.Interfaces.IRepository;

public interface IWalksRepository : IBaseRepository<Walks> 
{
    Task<PaginatedDataView<Walks>> GetPaginatedData(int pageNumber, int pageSize, int? lengthMin, int? lengthMax, int? difficultyFK, int? regionFK);
    
    Task<Walks> GetById(int id);
}