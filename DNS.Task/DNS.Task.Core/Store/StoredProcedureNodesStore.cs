using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DNS.Task.Core.Models;

namespace DNS.Task.Core.Store
{
	public class StoredProcedureNodesStore: AdoStoreBase, ICrudStore<Node, int>
	{
		public StoredProcedureNodesStore(AdoUnitOfWork unit) : base(unit)
		{
		}

		public async Task<int> AddAsync(Node entity, CancellationToken cancellationToken)
		{
			using (var reader = await CreateStoreCommand("[dbo].[sp_AddNode]",
				new SqlParameter("@ParentId", entity.ParentId),
				new SqlParameter("@Title", entity.Title),
				new SqlParameter("@NodeType", (int)entity.NodeType)).ExecuteReaderAsync(cancellationToken))
			{
				return reader.Read() ? reader.GetInt32(0) : -1;
			}
		}

		public async Task<bool> UpdateAsync(Node entity, CancellationToken cancellationToken)
		{
			var result = await CreateStoreCommand("[dbo].[sp_AddNode]",
				new SqlParameter("@id", entity.Id),
				new SqlParameter("@ParentId", entity.ParentId),
				new SqlParameter("@Title", entity.Title),
				new SqlParameter("@NodeType", (int) entity.NodeType)).ExecuteNonQueryAsync(cancellationToken);
			return result > 0;
		}

		public Task<Node> GetAsync(int key, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Node> GetAsync<TFilter>(TFilter filter, CancellationToken cancellationToken) where TFilter : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Node>> GetList(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Node>> GetList<TFilter>(TFilter filter, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ForceDelete(int key, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
