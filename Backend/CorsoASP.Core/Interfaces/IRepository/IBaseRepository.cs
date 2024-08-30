using CorsoASP.Core.Views;

namespace CorsoASP.Core.Interfaces.IRepository;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetById<TID>(TID id);
    Task<IEnumerable<T>> GetAll();
    Task<PaginatedDataView<T>> GetPaginatedData(int pageNumber, int pageSize);
    Task<bool> ExistsById<TKey>(TKey id) where TKey : IEquatable<TKey>;
    Task<T> Create(T model);
    Task<bool> Update(T model);
    Task<bool> Delete(int id);
}