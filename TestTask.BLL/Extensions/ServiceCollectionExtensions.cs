using Microsoft.Extensions.DependencyInjection;
using TestTask.BLL.Services;
using TestTask.BLL.Services.Interfaces;

namespace TestTask.BLL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBllServices(this IServiceCollection services)
		{
			services.AddScoped<IContactService, ContactService>();
			services.AddScoped<IContractorService, ContractorService>();

			return services;
		}
	}
}
