using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DNS.Task.Core.Models;
using DNS.Task.Core.Store;

namespace DNS.Task.Web
{
	class AdoModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.Register(c => new AdoUnitOfWork(ConfigurationManager.ConnectionStrings["DNS.Database"].ConnectionString))
				.As<IUnitOfWork, AdoUnitOfWork>()
				.InstancePerLifetimeScope()
				.OnRelease(unit =>
				{
					unit.Dispose();
				});

			builder.RegisterType<NodeCreator>()
				.SingleInstance();

			builder.RegisterType<StoredProcedureNodesStore>()
				.As<ICrudStore<Node>>()
				.InstancePerLifetimeScope();
		}
	}
}
