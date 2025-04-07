using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class GenericRepository<T>(StudentDbContext dbContext) 
		: IGenericRepository<T> where T : class
	{
		private readonly StudentDbContext _dbContext = dbContext;

		public async Task AddAsync(T entity, CancellationToken token = default)
		{
			await _dbContext.Set<T>().AddAsync(entity, token);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task AddManyAsync(IEnumerable<T> entities, CancellationToken token = default)
		{
			await _dbContext.Set<T>().AddRangeAsync(entities, token);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task DeleteAsync(T entity, CancellationToken token = default)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
		{
			var entities = await _dbContext.Set<T>().AsNoTracking().ToListAsync(token);
			return entities;
		}

		public async Task<T> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var entity = await _dbContext.Set<T>().FindAsync(Id, token);
			return entity!;
		}

		public async Task UpdateAsync(T entity, CancellationToken token = default)
		{
			_dbContext.Entry(entity).State = EntityState.Detached;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync(token);
		}
	}
}
