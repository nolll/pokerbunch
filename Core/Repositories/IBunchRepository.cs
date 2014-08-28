using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch GetBySlug(string slug);
        IList<Bunch> GetList();
        IList<Bunch> GetByUser(User user);
        Role GetRole(Bunch bunch, User user);
        Bunch Add(Bunch bunch);
        bool Save(Bunch bunch);
        Bunch GetById(int id);
    }
}