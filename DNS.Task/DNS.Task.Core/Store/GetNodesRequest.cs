namespace DNS.Task.Core.Store
{
	public class GetNodesRequest
	{
		public GetNodesRequest(int? parentId)
		{
			ParentId = parentId;
		}

		public int? ParentId { get; set; }
	}
}