using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IBunchService
    {
        Bunch Get(string slug);
        IList<Bunch> GetByUserId(string userId);
        IList<Bunch> GetList();
        string Add(Bunch bunch);
        void Save(Bunch bunch);
    }
}