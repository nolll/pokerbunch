using Core.Entities;

namespace Core.Services
{
    public interface IAuth
    {
        bool IsInRole(string slug, Role manager);
    }
}