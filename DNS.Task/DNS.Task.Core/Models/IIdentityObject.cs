namespace DNS.Task.Core.Models
{
	public interface IIdentityObject<TKey>
	{
		TKey Id { get; set; }
	}
}