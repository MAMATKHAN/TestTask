using Npgsql;

namespace TestTask.DAL.Repositories.Interfaces
{
	public interface IPgRepository
	{
		Task<NpgsqlConnection> GetConnection();
	}
}
