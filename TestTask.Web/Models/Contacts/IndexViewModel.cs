using TestTask.BLL.Models.Contacts;
using TestTask.BLL.Models.Contracts;

namespace TestTask.Web.Models.Contacts
{
    public class IndexViewModel
    {
        public long SelectedContractorId { get; set; }
        public IEnumerable<GetContactModel> Contacts { get; set; }
        public IEnumerable<GetContractorModel> Contractors { get; set; }
    }
}
