using TestTask.DAL.Entities;

namespace TestTask.DAL.Repositories.Interfaces
{
	public interface IContractorRepository
	{
		Task<long> Add(ContractorEntityV1 contractor, CancellationToken token);
		Task<ContractorEntityV1?> Get(long id, CancellationToken token);
		Task<ContractorEntityV1?> GetByName(string name, CancellationToken token);
		Task<ContractorEntityV1[]> GetAll(CancellationToken token);
		Task Update(ContractorEntityV1 contractor, CancellationToken token);
		Task Delete(long id, CancellationToken token);
	}
}
