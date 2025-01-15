using TestTask.BLL.Models.Contacts;
using TestTask.BLL.Models.Contracts;

namespace TestTask.Web.Models.Contacts
{
    public class EditContactViewModel
    {
        public EditContactDto Contact { get; set; }
        public IEnumerable<GetContractorModel>? Contractors { get; set; }
    }
}
