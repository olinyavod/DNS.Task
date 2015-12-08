using System;
using System.Threading;
using Autofac;
using DNS.Task.Core.Models;
using DNS.Task.Core.Store;
using NUnit.Framework;

namespace DNS.Task.Core.Tests
{
	[TestFixture]
	public class NodeTests
    {
		static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<AdoModule>();
			return builder.Build();
		}

		[TestFixture]
		public class AddNodeTests
		{
			[Test]
			public async System.Threading.Tasks.Task AddValidObject()
			{
				using (var container = CreateContainer())
				{
					var cancellationTokenSource = new CancellationTokenSource();
					var store = container.Resolve<ICrudStore<Node>>();
					var id = await store.AddAsync(new Node {Title = "Node1", NodeType = NodeType.File}, cancellationTokenSource.Token);
					Assert.AreNotEqual(-1, id);
				}
			}

			[Test]
			[ExpectedException(typeof(ArgumentNullException))]
			public async System.Threading.Tasks.Task AddNullObject()
			{
				using (var container = CreateContainer())
				{
					var cancellationTokenSource = new CancellationTokenSource();
					var store = container.Resolve<ICrudStore<Node>>();
					var id = await store.AddAsync(null, cancellationTokenSource.Token);
					Assert.AreNotEqual(-1, id);
				}
			}
		}
    }
}
