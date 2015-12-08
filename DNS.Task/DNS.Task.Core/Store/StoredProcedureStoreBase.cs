using System;
using System.Collections.Generic;

namespace DNS.Task.Core.Store
{
	public abstract class StoredProcedureStoreBase : AdoStoreBase
	{
		private readonly Dictionary<Type, Delegate> _executors;

		public StoredProcedureStoreBase(AdoUnitOfWork unit) : base(unit)
		{
			_executors = new Dictionary<Type, Delegate>();
		}

		protected void RegisterFilter<TFilter, TResult>(Func<TFilter, TResult> execute)
		{
			_executors.Add(typeof(TFilter), execute);
		}

		protected TResult Execute<TFilter, TResult>(TFilter filter)
		{
			return ((TResult) _executors[typeof (TFilter)].DynamicInvoke(filter));
		}
	}
}