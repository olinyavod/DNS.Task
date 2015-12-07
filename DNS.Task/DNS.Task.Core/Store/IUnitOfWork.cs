using System;

namespace DNS.Task.Core.Store
{
	public interface IUnitOfWork : IDisposable
	{
		void Rollback();
	}
}
