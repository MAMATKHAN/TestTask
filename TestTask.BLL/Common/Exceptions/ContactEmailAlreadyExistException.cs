
namespace TestTask.BLL.Common.Exceptions
{
    public class ContactEmailAlreadyExistException : Exception
    {
        public ContactEmailAlreadyExistException(string email) : base($"A contact with this email({email}) already exists.") { }
    }
}
