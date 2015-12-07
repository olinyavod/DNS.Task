using System;

namespace DNS.Task.Core.Models
{
	public class EntityBase : IIdentityObject<Guid>
	{
		public Guid Id { get; set; }

		public bool IsDeleted { get; set; }
	}
}