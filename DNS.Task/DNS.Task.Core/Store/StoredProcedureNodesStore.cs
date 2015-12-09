using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DNS.Task.Core.Models;

namespace DNS.Task.Core.Store
{
	public class StoredProcedureNodesStore: StoredProcedureStoreBase, ICrudStore<Node>
	{
		private readonly NodeCreator _creator;

		public StoredProcedureNodesStore(NodeCreator creator, AdoUnitOfWork unit) 
			: base(unit)
		{
			_creator = creator;
			RegisterFilter<GetNodeByIdRequest, Task<Node>>(GetAsync);
			RegisterFilter<GetNodesRequest, Task<IEnumerable<Node>>>(GetListAsync);
		}

		private async Task<IEnumerable<Node>> GetListAsync(GetNodesRequest request)
		{
			using (var reader = await CreateStoreCommand("[dbo].[sp_GetNodesList]",
				new SqlParameter("@ParentId", request.ParentId))
				.ExecuteReaderAsync()
				.ConfigureAwait(true))
			{
				return _creator.CreateFullTree(reader);
			}
		}

		private async Task<Node> GetAsync(GetNodeByIdRequest request)
		{
			using (var reader = await CreateStoreCommand(request.LoadChildren ? "[dbo].[sp_GetNodeByIdWithChilds]" : "[dbo].[sp_GetNodeById]",
				new SqlParameter("@Id", request.Id))
				.ExecuteReaderAsync()
				.ConfigureAwait(true))
			{
				if (request.LoadChildren)
					return _creator.CreateFullTree(reader).FirstOrDefault();
				return reader.Read() ? _creator.Create(reader) : null;
			}
		}

		public async Task<int> AddAsync(Node entity, CancellationToken cancellationToken)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			using (var reader = await CreateStoreCommand("[dbo].[sp_AddNode]",
				new SqlParameter("@ParentId", entity.ParentId),
				new SqlParameter("@Title", entity.Title),
				new SqlParameter("@NodeType", (int)entity.NodeType))
				.ExecuteReaderAsync(cancellationToken)
				.ConfigureAwait(true))
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
				new SqlParameter("@NodeType", (int) entity.NodeType))
				.ExecuteNonQueryAsync(cancellationToken)
				.ConfigureAwait(true);
			return result > 0;
		}

		public async Task<Node> GetAsync(int key, CancellationToken cancellationToken)
		{
			return await Execute<GetNodeByIdRequest, Task<Node>>(new GetNodeByIdRequest(key));
		}

		public async Task<Node> GetAsync<TFilter>(TFilter filter, CancellationToken cancellationToken) where TFilter : class
		{
			return await Execute<TFilter, Task<Node>>(filter);
		}

		public async Task<IEnumerable<Node>> GetListAsync(CancellationToken cancellationToken)
		{
			using (var reader = await CreateStoreCommand("[dbo].[sp_GetAllNodesList]")
				.ExecuteReaderAsync(cancellationToken)
				.ConfigureAwait(true))
				return _creator.CreateFullTree(reader);
		}

		public async Task<IEnumerable<Node>> GetListAsync<TFilter>(TFilter filter, CancellationToken cancellationToken)
		{
			return await Execute<TFilter, Task<IEnumerable<Node>>>(filter);
		}

		public async Task<bool> ForceDeleteAsync(int key, CancellationToken cancellationToken)
		{
			var result = await CreateStoreCommand("[dbo].[sp_DeleteNode]", new SqlParameter("@Id", key))
				.ExecuteNonQueryAsync(cancellationToken)
				.ConfigureAwait(true);
			return result > 0;
		}
	}
}
