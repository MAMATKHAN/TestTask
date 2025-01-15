using TestTask.BLL.Common.Exceptions;
using TestTask.BLL.Models.Contacts;
using TestTask.BLL.Services.Interfaces;
using TestTask.DAL.Entities;
using TestTask.DAL.Repositories.Interfaces;

namespace TestTask.BLL.Services
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _contactRepository;
		private readonly IContractorRepository _contractorRepository;

		public ContactService(IContactRepository contactRepository, IContractorRepository contractorRepository)
		{
			_contactRepository = contactRepository;
			_contractorRepository = contractorRepository;
		}

		public async Task<long> CreateContact(CreateContactModel model, CancellationToken token)
		{
			var contractor = await _contractorRepository.Get(model.ContractorId, token);
			var emailIsContains = (await _contactRepository.GetByEmail(model.Email, token)) != null;

			if (contractor == null) throw new NotFoundException(model.ContractorId);
			if (emailIsContains) throw new ContactEmailAlreadyExistException(model.Email);

			var contact = new ContactEntityV1
			{
				ContractorId = model.ContractorId,
				FullName = model.FullName,
				Email = model.Email,
				CreatedAt = DateTimeOffset.UtcNow,
				UpdatedAt = DateTimeOffset.UtcNow,
			};

			var contactId = await _contactRepository.Add(contact, token);

			return contactId;
		}

		public async Task<GetContactModel> GetContact(long contactId, CancellationToken token)
		{
			var contact = await _contactRepository.Get(contactId, token);

			if (contact == null) throw new NotFoundException(contactId);

			var contractor = await _contractorRepository.Get(contact.ContractorId, token);

			var model = new GetContactModel
			{
				ContactId = contact.Id,
				ContractorId = contact.ContractorId,
				FullName = contact.FullName,
				Email = contact.Email,
				ContractorName = contractor.Name,
				CreatedAt = contact.CreatedAt,
				UpdatedAt = contact.UpdatedAt,
			};

			return model;
		}

		public async Task<GetContactModel[]> GetAll(CancellationToken token)
		{
			var contacts = await _contactRepository.GetAll(token);
			GetContactModel[] models = new GetContactModel[contacts.Length];

			for (int i = 0; i < contacts.Length; i++)
			{
				var contractorName = (await _contractorRepository.Get(contacts[i].ContractorId, token)).Name;
				models[i] = new GetContactModel
				{
					ContactId = contacts[i].Id,
					ContractorId = contacts[i].ContractorId,
					FullName = contacts[i].FullName,
					Email = contacts[i].Email,
					CreatedAt = contacts[i].CreatedAt,
					UpdatedAt = contacts[i].UpdatedAt,
					ContractorName = contractorName,
				};
			}

			//.Select(c => new GetContactModel
			//{
			//	ContactId= c.Id,
			//	Fullname= c.Fullname,
			//	Email= c.Email,
			//	CreatedAt= c.CreatedAt,
			//	UpdatedAt= c.UpdatedAt,
			//}).ToArray();

			return models;
		}



		public async Task UpdateContact(UpdateContactModel model, CancellationToken token)
		{
			var contact = await _contactRepository.Get(model.ContactId, token);
			var contractor = await _contractorRepository.Get(model.ContractorId, token);
            var contactByEmail = await _contactRepository.GetByEmail(model.Email, token);

            if (contact == null) throw new NotFoundException(model.ContactId);
			if (contractor == null) throw new NotFoundException(model.ContractorId);
            if (contactByEmail != null && contact.Id != contactByEmail.Id) throw new ContactEmailAlreadyExistException(model.Email);

            var entity = new ContactEntityV1
			{
				Id = model.ContactId,
				ContractorId = model.ContractorId,
				FullName = model.FullName,
				Email = model.Email,
				UpdatedAt = DateTimeOffset.UtcNow,
			};

			await _contactRepository.Update(entity, token);
		}

		public async Task DeleteContact(long contactId, CancellationToken token)
		{
			var contact = await _contactRepository.Get(contactId, token);

			if (contact == null) throw new NotFoundException(contactId);

			await _contactRepository.Delete(contact.Id, token);
		}

        public async Task<GetContactModel[]> GetContactsByContractorId(long contractorId, CancellationToken token)
        {
			var contractor = await _contractorRepository.Get(contractorId, token);

			if (contractor == null) throw new NotFoundException(contractorId);

            var contacts = await _contactRepository.GetByContractorId(contractorId, token);
			var models = contacts.Select(c => new GetContactModel
			{
				ContactId = c.Id,
				ContractorId = c.ContractorId,
				FullName = c.FullName,
				Email = c.Email,
				CreatedAt = c.CreatedAt,
				UpdatedAt = c.UpdatedAt,
				ContractorName = contractor.Name,
			}).ToArray();
           

			return models;
        }
    }
}
