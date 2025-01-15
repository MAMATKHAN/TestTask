
namespace TestTask.BLL.Models.Contracts
{
    public class GetContractorModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
