
namespace TestTask.BLL.Models.Contacts
{
    public class GetContactModel
    {
        public long ContactId { get; set; }
        public long ContractorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContractorName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
