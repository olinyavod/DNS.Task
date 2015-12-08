namespace DNS.Task.Core.Store
{
	public class GetNodeByIdRequest
	{
		public GetNodeByIdRequest(int id)
		{
			Id = id;
		}

		public int Id { get; set; }

		public bool LoadChildren { get; set; }
	}
}