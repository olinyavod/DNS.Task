namespace DNS.Task.Core.Store
{
	public interface IEntityCreator <TEntity, TReader>
		where TEntity:class
	{
		TEntity Create(TReader reader, string prefix = null);
	}
}
