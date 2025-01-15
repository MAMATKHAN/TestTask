using TestTask.BLL.Models.Contacts;

namespace TestTask.BLL.Services.Interfaces
{
    public interface IContactService
	{
		Task<long> CreateContact(CreateContactModel model, CancellationToken token);
		Task<GetContactModel> GetContact(long contactId, CancellationToken token);
		Task<GetContactModel[]> GetAll(CancellationToken token);
		Task<GetContactModel[]> GetContactsByContractorId(long contractorId, CancellationToken token);
		Task UpdateContact(UpdateContactModel model, CancellationToken token);
		Task DeleteContact(long contactId, CancellationToken token);
	}
}
