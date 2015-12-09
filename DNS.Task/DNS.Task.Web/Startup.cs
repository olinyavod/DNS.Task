using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Owin;

namespace DNS.Task.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<AdoModule>();

			app.UseAutofacMiddleware(builder.Build());
		}
	}
}
