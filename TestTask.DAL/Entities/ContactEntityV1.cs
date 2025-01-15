
namespace TestTask.DAL.Entities
{
	public class ContactEntityV1
	{
		public long Id { get; set; }
		public long ContractorId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
