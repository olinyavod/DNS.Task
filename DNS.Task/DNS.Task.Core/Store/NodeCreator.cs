using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DNS.Task.Core.Models;

namespace DNS.Task.Core.Store
{
	public class NodeCreator: EntityCreatorBase<Node>
	{
		public override Node Create(IDataReader reader, string prefix = null)
		{
			return new Node
			{
				Id = GetValue(reader, -1, prefix, "Id"),
				ParentId = GetValue<int?>(reader, null, prefix, "ParentId"),
				NodeType = GetValue(reader, NodeType.File, prefix, "NodeType"),
				Title = GetValue(reader, String.Empty, prefix, "Title")
			};
		}

		public IEnumerable<Node> CreateFullTree(IDataReader reader, string prefix = null)
		{
			var result = new Dictionary<int, Node>();
			while (reader.Read())
			{
				var node = Create(reader, prefix);
				if(result.ContainsKey(node.Id))
					continue;
				
				result.Add(node.Id, node);
				Node parent;
				if (result.TryGetValue(node.ParentId.GetValueOrDefault(-1), out parent)) 
					parent.Children.Add(node);
			}
			return result.Values.Where(i => i.ParentId == null).ToArray();
		}
	}
}
