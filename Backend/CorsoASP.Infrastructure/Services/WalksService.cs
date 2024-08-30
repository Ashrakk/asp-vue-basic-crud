using System.Numerics;
using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Mapping;
using CorsoASP.Core.Models;
using CorsoASP.Core.Views;

namespace CorsoASP.Infrastructure.Services;

public class WalksService : IWalksService
{
    private readonly IWalksRepository _walksRepository;

    public WalksService(IWalksRepository walksRepo)
    {
        _walksRepository = walksRepo;
    }
    
    public async Task<IEnumerable<WalksView>> GetWalks()
    {
        return WalksMap.ModelToView(await _walksRepository.GetAll());
    }
    public async Task<WalksDetailView?> GetWalk(int id)
    {
        return WalksDetailMap.ModelToView( await _walksRepository.GetById(id));
    }
    
    public async Task<PaginatedDataView<WalksDetailView>> GetPaginatedWalks(int pageNumber, int pageSize, int? lengthMin, int? lengthMax, int? difficultyFK, int? regionFK)
    {
        var res = await _walksRepository.GetPaginatedData(pageNumber, pageSize, lengthMin, lengthMax, difficultyFK, regionFK);
        return new PaginatedDataView<WalksDetailView>(WalksDetailMap.ModelToView(res.Data), res.TotalCount);
    }

    public async Task<bool> ExistsById(int id)
    {
        return await _walksRepository.ExistsById(id);
    }

    public async Task<WalksView> Create(WalksView view)
    {
        var model = WalksMap.ViewToModel(view);
        model.CreateDate = DateTime.UtcNow;
        model.UpdateDate = DateTime.UtcNow;
        
        return WalksMap.ModelToView(await _walksRepository.Create(model));
    }

    public async Task<bool> Update(WalksView view)
    {
        var model = WalksMap.ViewToModel(view);
        model.UpdateDate = DateTime.UtcNow;
        
        return await _walksRepository.Update(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _walksRepository.Delete(id);
    }
}