using System;
using System.Data;
using System.Linq;

namespace DNS.Task.Core.Store
{
	public abstract class EntityCreatorBase<TEntity> : IEntityCreator<TEntity, IDataReader> 
		where TEntity : class
	{
		protected TValue GetValue<TValue>(IDataReader reader, TValue defaultValue, params string[] name)
		{
			try
			{
				return ((TValue) reader[string.Join(".", name.Where(i => !string.IsNullOrWhiteSpace(i)))]);
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}

		public abstract TEntity Create(IDataReader reader, string prefix = null);
	}
}