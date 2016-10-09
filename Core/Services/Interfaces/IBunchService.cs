using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IBunchService
    {
        Bunch Get(string slug);
        IList<Bunch> GetByUserId(int userId);
        IList<Bunch> GetList();
        int Add(Bunch bunch);
        void Save(Bunch bunch);
    }
}