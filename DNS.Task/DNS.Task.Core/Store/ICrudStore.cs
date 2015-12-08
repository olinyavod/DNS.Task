using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DNS.Task.Core.Models;

namespace DNS.Task.Core.Store
{
	public interface ICrudStore<TEntity, TKey>
		where TEntity:IIdentityObject<TKey>
	{
		Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken);

		Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

		Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken);

		Task<TEntity> GetAsync<TFilter>(TFilter filter, CancellationToken cancellationToken)
			where TFilter : class;

		Task<IEnumerable<TEntity>> GetList(CancellationToken cancellationToken);

		Task<IEnumerable<TEntity>> GetList<TFilter>(TFilter filter, CancellationToken cancellationToken);

		Task<bool> ForceDelete(TKey key, CancellationToken cancellationToken);
	}

	public interface ICrudStore<TEntity> : ICrudStore<TEntity, int> 
		where TEntity : IIdentityObject<int>
	{
		
	}
}
