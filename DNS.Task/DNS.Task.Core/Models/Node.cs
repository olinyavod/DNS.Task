using System;
using System.Collections.Generic;

namespace DNS.Task.Core.Models
{
	public class Node : EntityBase
	{
	    public Node()
	    {
		    Children = new List<Node>();
	    }
1

		public Guid ParentId { get; set; }

	    public string Title { get; set; }

	    public NodeType NodeType { get; set; }

	    public ICollection<Node> Children { get; set; }
    }
}
