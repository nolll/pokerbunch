using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch GetById(int id);
        Bunch GetBySlug(string slug);
        IList<Bunch> GetList();
        IList<Bunch> GetByUser(User user);
        Role GetRole(int bunchId, int userId);
        int Add(Bunch bunch);
        bool Save(Bunch bunch);
    }
}