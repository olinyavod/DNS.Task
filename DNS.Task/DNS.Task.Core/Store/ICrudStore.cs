using System.Collections.Generic;
using DNS.Task.Core.Models;

namespace DNS.Task.Core.Store
{
	public interface ICrudStore<TEntity, TKey>
		where TEntity:IIdentityObject<TKey>
	{
		TKey Add(TEntity entity);

		bool Update(TEntity entity);

		TEntity Get(TKey key);

		TEntity Get<TFilter>(TFilter filter)
			where TFilter : class;

		IEnumerable<TEntity> GetList();

		IEnumerable<TEntity> GetList<TFilter>(TFilter filter);

		bool ForceDelete(TKey key);
	}
}
