using TestTask.BLL.Models.Contracts;

namespace TestTask.Web.Models.Contacts
{
    public class AddContactViewModel
    {
        public AddContactDto Contact { get; set; }
        public IEnumerable<GetContractorModel>? Contractors { get; set; }
    }
}
