using System.Data;
using System.Data.SqlClient;

namespace DNS.Task.Core.Store
{
	public class AdoUnitOfWork : IUnitOfWork
	{
		private readonly SqlConnection _connection;
		private readonly SqlTransaction _transaction;

		public AdoUnitOfWork(string connectionString)
		{
			_connection = new SqlConnection(connectionString);
			_transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		~AdoUnitOfWork()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_transaction.Commit();
				_transaction.Dispose();
				_connection.Close();
				_connection.Dispose();
			}
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}

		public SqlCommand CreateSqlCommand(string sql, params SqlParameter[] parameters)
		{
			var command = _connection.CreateCommand();
			command.Transaction = _transaction;
			command.CommandType = CommandType.Text;
			if (parameters != null)
				command.Parameters.AddRange(parameters);
			command.CommandText = sql;
			return command;
		}

		public SqlCommand CreateStoreCommand(string storeName, params SqlParameter[] parameters)
		{
			var command = _connection.CreateCommand();
			command.Transaction = _transaction;
			command.CommandType = CommandType.StoredProcedure;
			if (parameters != null)
				command.Parameters.AddRange(parameters);
			command.CommandText = storeName;
			return command;
		}
	}
}
