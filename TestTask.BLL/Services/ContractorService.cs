using TestTask.BLL.Common.Exceptions;
using TestTask.BLL.Models.Contracts;
using TestTask.BLL.Services.Interfaces;
using TestTask.DAL.Entities;
using TestTask.DAL.Repositories.Interfaces;

namespace TestTask.BLL.Services
{
	public class ContractorService : IContractorService
	{
		private readonly IContractorRepository _contractorRepository;

		public ContractorService(IContractorRepository contractorRepository)
		{
			_contractorRepository = contractorRepository;
		}

		public async Task<long> CreateContractor(CreateContractorModel model, CancellationToken token)
		{
			var nameIsContains = (await _contractorRepository.GetByName(model.Name, token)) != null;

			if (nameIsContains) throw new ContractorNameAlreadyExistException(model.Name);

			var contractor = new ContractorEntityV1
			{
				Name = model.Name,
				CreatedAt = DateTimeOffset.UtcNow,
				UpdatedAt = DateTimeOffset.UtcNow,
			};

			var contractorId = await _contractorRepository.Add(contractor, token);

			return contractorId;
		}

		public async Task<GetContractorModel[]> GetAll(CancellationToken token)
		{
			var contractors = (await _contractorRepository.GetAll(token))
				.Select(c => new GetContractorModel
				{
					Id = c.Id,
					Name = c.Name,
					CreatedAt = c.CreatedAt,
					UpdatedAt = c.UpdatedAt,
				}).ToArray();

			return contractors;
		}

		public async Task<GetContractorModel> GetContractor(long contractorId, CancellationToken token)
		{
			var contractor = await _contractorRepository.Get(contractorId, token);

			if (contractor == null) throw new NotFoundException(contractorId);

			var model = new GetContractorModel
			{
				Id = contractor.Id,
				Name = contractor.Name,
				CreatedAt = contractor.CreatedAt,
				UpdatedAt = contractor.UpdatedAt,
			};

			return model;
		}

		public async Task UpdateContractor(UpdateContractorModel model, CancellationToken token)
		{
			var contractor = await _contractorRepository.Get(model.ContractorId, token);
            var nameIsContains = (await _contractorRepository.GetByName(model.Name, token)) != null;

            if (contractor == null) throw new NotFoundException(model.ContractorId);
            if (nameIsContains) throw new ContractorNameAlreadyExistException(model.Name);

            var entity = new ContractorEntityV1
			{
				Id = model.ContractorId,
				UpdatedAt = DateTimeOffset.UtcNow,
			};

			await _contractorRepository.Update(entity, token);
		}

		public async Task DeleteContractor(long contractorId, CancellationToken token)
		{
			var contractor = await _contractorRepository.Get(contractorId, token);

			if (contractor == null) throw new NotFoundException(contractorId);

			await _contractorRepository.Delete(contractorId, token);
		}
	}
}
