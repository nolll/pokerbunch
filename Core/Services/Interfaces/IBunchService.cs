using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IBunchService
    {
        Bunch Get(string slug);
        IList<SmallBunch> GetByUserId(string userId);
        IList<SmallBunch> GetList();
        Bunch Add(Bunch bunch);
        Bunch Save(Bunch bunch);
    }
}