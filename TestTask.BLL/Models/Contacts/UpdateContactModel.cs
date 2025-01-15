
namespace TestTask.BLL.Models.Contacts
{
    public class UpdateContactModel
    {
        public long ContactId { get; set; }
        public long ContractorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
