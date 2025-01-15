using Dapper;
using Microsoft.Extensions.Options;
using TestTask.DAL.Entities;
using TestTask.DAL.Options;
using TestTask.DAL.Repositories.Interfaces;

namespace TestTask.DAL.Repositories
{
	public class ContractorRepository : PgRepository, IContractorRepository
	{
		public ContractorRepository(IOptions<DalOptions> dalOptions)
			: base(dalOptions.Value) { }

		public async Task<long> Add(ContractorEntityV1 contractor, CancellationToken token)
		{
			const string sql = @"
insert into contractors(name, created_at, updated_at)
	   values(@Name, @CreatedAt, @UpdatedAt)
returning id;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, contractor, cancellationToken: token);
			var id = await connection.QuerySingleAsync<long>(cmd);

			return id;
		}

		public async Task<ContractorEntityV1?> Get(long id, CancellationToken token)
		{
			const string sql = @"
select id
	 , name
	 , created_at
	 , updated_at
  from contractors
 where id = @Id;
";
			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, new {Id = id}, cancellationToken: token);

			return await connection.QuerySingleOrDefaultAsync<ContractorEntityV1>(cmd);
		}

        public async Task<ContractorEntityV1?> GetByName(string name, CancellationToken token)
        {
            const string sql = @"
select id
	 , name
	 , created_at
	 , updated_at
  from contractors
 where name = @Name;
";
            await using var connection = await GetConnection();
            var cmd = new CommandDefinition(sql, new { Name = name }, cancellationToken: token);

            return await connection.QuerySingleOrDefaultAsync<ContractorEntityV1>(cmd);
        }

        public async Task<ContractorEntityV1[]> GetAll(CancellationToken token)
		{
			const string sql = @"
select id
	 , name
	 , created_at
	 , updated_at
  from contractors;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, cancellationToken: token);

			return (await connection.QueryAsync<ContractorEntityV1>(cmd)).ToArray();
		}

		public async Task Update(ContractorEntityV1 contract, CancellationToken token)
		{
			const string sql = @"
update contracts
   set name = @Name
	 , updated_at = @UpdatedAt
 where id =	@Id;
";

			await using var conneciton = await GetConnection();
			var cmd = new CommandDefinition(sql, contract, cancellationToken: token);

			await conneciton.ExecuteAsync(cmd);
		}

		public async Task Delete(long id, CancellationToken token)
		{
			const string sql = @"
delete from contractors
 where id = @id;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, new { Id = id }, cancellationToken: token);

			await connection.ExecuteAsync(cmd);
		}
	}
}
