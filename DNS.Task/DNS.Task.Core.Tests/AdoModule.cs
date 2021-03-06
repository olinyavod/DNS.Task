﻿using System.Configuration;
using Autofac;
using DNS.Task.Core.Models;
using DNS.Task.Core.Store;

namespace DNS.Task.Core.Tests
{
	class AdoModule:Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.Register(c => new AdoUnitOfWork(ConfigurationManager.ConnectionStrings["DNS.Database"].ConnectionString))
				.As<IUnitOfWork, AdoUnitOfWork>()
				.InstancePerLifetimeScope()
				.OnRelease(unit =>
				{
					unit.Rollback();
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
