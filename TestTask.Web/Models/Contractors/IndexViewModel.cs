using TestTask.BLL.Models.Contracts;

namespace TestTask.Web.Models.Contractors
{
    public class IndexViewModel
    {
        public IEnumerable<GetContractorModel> Contractors { get; set; }
    }
}
