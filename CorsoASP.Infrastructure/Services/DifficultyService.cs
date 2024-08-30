using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Mapping;
using CorsoASP.Core.Views;

namespace CorsoASP.Infrastructure.Services;

public class DifficultyService: IDifficultyService
{
    private readonly IDifficultyRepository _difficultyRepository;

    public DifficultyService(IDifficultyRepository diffRepo)
    {
        _difficultyRepository = diffRepo;
    }
    
    public async Task<DifficultyView> GetDifficulty(int id)
    {
        var result = await _difficultyRepository.GetById(id);
        if (result != null)
            return DifficultyMap.ModelToView(result);
        else
            return null;
    }

    public async Task<IEnumerable<DifficultyView>> GetDifficulties()
    {
        return DifficultyMap.ModelToView(await _difficultyRepository.GetAll());
    }
}