using CorsoASP.Core.Views;

namespace CorsoASP.Core.Interfaces.IService;

public interface IDifficultyService
{
    Task<DifficultyView> GetDifficulty(int id);
    Task<IEnumerable<DifficultyView>> GetDifficulties();
}