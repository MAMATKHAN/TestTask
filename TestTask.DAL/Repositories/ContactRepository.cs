using Dapper;
using Microsoft.Extensions.Options;
using TestTask.DAL.Entities;
using TestTask.DAL.Options;
using TestTask.DAL.Repositories.Interfaces;

namespace TestTask.DAL.Repositories
{
	public class ContactRepository : PgRepository, IContactRepository
	{
		public ContactRepository(IOptions<DalOptions> dalOptions)
			: base(dalOptions.Value) { }

		public async Task<long> Add(ContactEntityV1 contact, CancellationToken token)
		{
			const string sql = @"
insert into contacts(contractor_id, full_name, email, created_at, updated_at)
	   values(@ContractorId, @FullName, @Email, @CreatedAt, @UpdatedAt)
returning id;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, contact, cancellationToken: token);
			var id = await connection.QuerySingleAsync<long>(cmd);

			return id;
		}

		public async Task<ContactEntityV1?> Get(long id, CancellationToken token)
		{
			const string sql = @"
select id
	 , contractor_id
	 , full_name
	 , email
	 , created_at
	 , updated_at
  from contacts
 where id = @Id;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, new {Id = id}, cancellationToken: token);

			return await connection.QuerySingleOrDefaultAsync<ContactEntityV1>(cmd);
		}

		public async Task<ContactEntityV1[]> GetAll(CancellationToken token)
		{
			const string sql = @"
select id
	 , contractor_id
	 , full_name
	 , email
	 , created_at
	 , updated_at
  from contacts;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, cancellationToken: token);
			
			return (await connection.QueryAsync<ContactEntityV1>(cmd)).ToArray();
		}

		public async Task Update(ContactEntityV1 contact, CancellationToken token)
		{
			const string sql = @"
update contacts
   set contractor_id = @ContractorId
	 , full_name = @FullName
	 , email = @Email
	 , updated_at = @UpdatedAt
 where id =	@Id;
";

			await using var conneciton = await GetConnection();
			var cmd = new CommandDefinition(sql, contact, cancellationToken: token);
			
			await conneciton.ExecuteAsync(cmd);
		}

		public async Task Delete(long id, CancellationToken token)
		{
			const string sql = @"
delete from contacts
 where id = @id;
";

			await using var connection = await GetConnection();
			var cmd = new CommandDefinition(sql, new { Id = id }, cancellationToken: token);

			await connection.ExecuteAsync(cmd);
		}

        public async Task<ContactEntityV1[]> GetByContractorId(long contractorId, CancellationToken token)
        {
            const string sql = @"
select id
	 , contractor_id
	 , full_name
	 , email
	 , created_at
	 , updated_at
  from contacts
 where contractor_id = @ContractorId;
";

            await using var connection = await GetConnection();
            var cmd = new CommandDefinition(sql, new { ContractorId = contractorId } ,cancellationToken: token);

            return (await connection.QueryAsync<ContactEntityV1>(cmd)).ToArray();
        }

        public async Task<ContactEntityV1?> GetByEmail(string email, CancellationToken token)
        {
            const string sql = @"
select id
	 , contractor_id
	 , full_name
	 , email
	 , created_at
	 , updated_at
  from contacts
 where email = @Email;
";

            await using var connection = await GetConnection();
            var cmd = new CommandDefinition(sql, new { Email = email }, cancellationToken: token);

            return await connection.QuerySingleOrDefaultAsync<ContactEntityV1>(cmd);
        }
    }
}
