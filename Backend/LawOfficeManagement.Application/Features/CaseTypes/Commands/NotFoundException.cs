// NotFoundException
namespace LawOfficeManagement.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"الكيان \"{name}\" ({key}) غير موجود.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}