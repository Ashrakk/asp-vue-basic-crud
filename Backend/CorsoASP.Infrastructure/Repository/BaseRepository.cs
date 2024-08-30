using CorsoASP.Core.Interfaces;
using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Views;
using CorsoASP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CorsoASP.Infrastructure.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetById<TID>(TID id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<PaginatedDataView<T>> GetPaginatedData(int pageNumber, int pageSize)
    {
        var query = _context.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking();

        var data = await query.ToListAsync();
        var count = await _context.Set<T>().CountAsync();

        return new PaginatedDataView<T>(data, count);
    }
    
    public async Task<bool> ExistsById<TKey>(TKey id) where TKey : IEquatable<TKey>
    {
        return await _context.Set<T>().AnyAsync(e => EF.Property<TKey>(e, "ID").Equals(id));
    }
    
    public async Task<T> Create(T model)
    {
        await _context.Set<T>().AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> Update(T model)
    {
        _context.Set<T>().Update(model);
        var changes = await _context.SaveChangesAsync();

        if(changes > 0)
            return true;
        return false;
    }

    public async Task<bool> Delete(int id)
    {
        var model = await _context.Set<T>().FindAsync(id);
        if (model == null)
            return false;

        _context.Set<T>().Remove(model);
        var changes = await _context.SaveChangesAsync();

        if(changes > 0)
            return true;
        return false;
    }
}