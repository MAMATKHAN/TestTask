using TestTask.BLL.Models.Contracts;

namespace TestTask.BLL.Services.Interfaces
{
	public interface IContractorService
	{
		Task<long> CreateContractor(CreateContractorModel model, CancellationToken token);
		Task<GetContractorModel> GetContractor(long contractorId, CancellationToken token);
		Task<GetContractorModel[]> GetAll(CancellationToken token);
		Task UpdateContractor(UpdateContractorModel model, CancellationToken token);
		Task DeleteContractor(long contractorId, CancellationToken token);
	}
}
