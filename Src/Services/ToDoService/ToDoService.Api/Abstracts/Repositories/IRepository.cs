using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ToDoService.Api.Abstracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, params string[] Includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, params string[] Includes);

        //Task<List<T>> GetAllPaginatedAsync(int page, int size, Expression<Func<T, bool>> expression = null,
        //    params string[] Includes);

        //Task<int> GetTotalCountAsync(Expression<Func<T, bool>> expression = null);

        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
