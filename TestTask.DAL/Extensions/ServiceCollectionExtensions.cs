using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.DAL.Infrastructure;
using TestTask.DAL.Options;
using TestTask.DAL.Repositories;
using TestTask.DAL.Repositories.Interfaces;


namespace TestTask.DAL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDalRepositories(this IServiceCollection services)
		{
			services.AddScoped<IContactRepository, ContactRepository>();
			services.AddScoped<IContractorRepository, ContractorRepository>();

			return services;
		}

		public static IServiceCollection AddDalInfrastructute(this IServiceCollection services, IConfigurationRoot config)
		{
			
            services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));

            Postgres.MapCompositeTypes(services, config);
			Postgres.AddMigrations(services);

			return services;
		}
	}
}
