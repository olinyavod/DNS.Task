using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNS.Task.Core.Models;

namespace DNS.Task.Core.Store
{
	public class StoredProcedureNodesStore: AdoStoreBase, ICrudStore<Node, int>
	{
		public StoredProcedureNodesStore(AdoUnitOfWork unit) : base(unit)
		{
		}

		public int Add(Node entity)
		{
			throw new NotImplementedException();
		}

		public bool Update(Node entity)
		{
			throw new NotImplementedException();
		}

		public Node Get(int key)
		{
			throw new NotImplementedException();
		}

		public Node Get<TFilter>(TFilter filter) where TFilter : class
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Node> GetList()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Node> GetList<TFilter>(TFilter filter)
		{
			throw new NotImplementedException();
		}

		public bool ForceDelete(int key)
		{
			throw new NotImplementedException();
		}
	}
}
