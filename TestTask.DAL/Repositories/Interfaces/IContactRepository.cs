using TestTask.DAL.Entities;

namespace TestTask.DAL.Repositories.Interfaces
{
	public interface IContactRepository
	{
		Task<long> Add(ContactEntityV1 contacts, CancellationToken token);
		Task<ContactEntityV1?> Get(long id, CancellationToken token);
		Task<ContactEntityV1?> GetByEmail(string email, CancellationToken token);
		Task<ContactEntityV1[]> GetAll(CancellationToken token);
		Task<ContactEntityV1[]> GetByContractorId(long contractorId, CancellationToken token);
		Task Update(ContactEntityV1 contact, CancellationToken token);
		Task Delete(long id, CancellationToken token);
	}
}
