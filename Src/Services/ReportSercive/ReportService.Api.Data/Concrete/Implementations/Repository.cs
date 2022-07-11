using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReportService.Api.Core.Abstracts.Repositories;
using ReportService.Api.Data.DAL;

namespace ReportService.Api.Data.Concrete.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, params string[] Includes)
        {
            var query = GetQuery(Includes);
            return expression is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, params string[] Includes)
        {
            // add no traking
            var query = GetQuery(Includes);
            return expression is null
                ? await query.ToListAsync()
                : await query.Where(expression).ToListAsync();
        }



        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
        }



        public void Remove(T entity)
        {
            Table.Remove(entity);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }



        private IQueryable<T> GetQuery(params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
