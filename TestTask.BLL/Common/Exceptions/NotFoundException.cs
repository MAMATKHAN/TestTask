
namespace TestTask.BLL.Common.Exceptions
{
	public class NotFoundException : Exception
    {
		public NotFoundException(long id) : base($"Entity({id}) not found") { }
	}
}
