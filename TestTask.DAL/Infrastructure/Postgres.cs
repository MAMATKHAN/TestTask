using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.NameTranslation;
using TestTask.DAL.Entities;
using FluentMigrator.Runner;
using Microsoft.Extensions.Options;
using TestTask.DAL.Options;
using Microsoft.Extensions.Configuration;

namespace TestTask.DAL.Infrastructure
{
	public static class Postgres
	{
		private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

		public static void MapCompositeTypes(IServiceCollection services, IConfigurationRoot config)
		{
			var connectionString = config.GetSection("DalOptions").GetSection("PostgresConnectionString").Value;
            var builder = new NpgsqlDataSourceBuilder(connectionString);

			Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

			builder.MapComposite<ContactEntityV1>("contact_v1", Translator);
			builder.MapComposite<ContractorEntityV1>("contractor_v1", Translator);
		}

		public static void AddMigrations(IServiceCollection services)
		{
			services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                {
                    var cfg = s.GetRequiredService<IOptions<DalOptions>>();
                    return cfg.Value.PostgresConnectionString;
                })
                    .ScanIn(typeof(Postgres).Assembly).For.Migrations()
				)
				.AddLogging(lb => lb.AddFluentMigratorConsole());
		}
	}
}
