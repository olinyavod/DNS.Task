using System.Data.SqlClient;

namespace DNS.Task.Core.Store
{
	public abstract class AdoStoreBase
	{
		private readonly AdoUnitOfWork _unit;

		public AdoStoreBase(AdoUnitOfWork unit)
		{
			_unit = unit;
		}

		protected SqlCommand CreateSqlCommand(string sql, params SqlParameter[] parameters)
		{
			return _unit.CreateSqlCommand(sql, parameters);
		}

		protected SqlCommand CreateStoreCommand(string store, params SqlParameter[] parameters)
		{
			return _unit.CreateStoreCommand(store, parameters);
		}

		protected void Rollback()
		{
			_unit.Rollback();
		}
	}
}
