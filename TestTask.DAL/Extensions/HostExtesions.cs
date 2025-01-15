using Microsoft.Extensions.Hosting;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.DAL.Extensions
{
	public static class HostExtesions
	{
		public static IHost MigrateUp(this IHost app)
		{
			using var scope = app.Services.CreateScope();
			var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
			//runner.MigrateDown(0);
			runner.MigrateUp();
			return app;
		}
	}
}
