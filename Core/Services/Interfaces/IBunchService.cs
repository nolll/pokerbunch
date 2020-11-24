using Core.Entities;

namespace Core.Services
{
    public interface IBunchService
    {
        Bunch Get(string id);
        Bunch Update(Bunch bunch);
    }
}