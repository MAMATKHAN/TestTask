using Npgsql;
using System.Transactions;
using TestTask.DAL.Repositories.Interfaces;
using TestTask.DAL.Options;

namespace TestTask.DAL.Repositories
{
	public class PgRepository : IPgRepository
	{
		private readonly DalOptions _dalOptions;

		public PgRepository(DalOptions dalOptions)
		{
			_dalOptions = dalOptions;
		}

		public async Task<NpgsqlConnection> GetConnection()
		{
			if (Transaction.Current is not null &&
			Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
			{
				throw new TransactionAbortedException("Transaction was aborted (probably by user cancellation request)");
			}

			var connection = new NpgsqlConnection(_dalOptions.PostgresConnectionString);
			await connection.OpenAsync();

			connection.ReloadTypes();

			return connection;
		}
	}
}
