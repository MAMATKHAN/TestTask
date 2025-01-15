
namespace TestTask.BLL.Common.Exceptions
{
    public class ContractorNameAlreadyExistException : Exception
    {
        public ContractorNameAlreadyExistException(string name) : base($"A contractor with this name({name}) already exists.") { }
    }
}
