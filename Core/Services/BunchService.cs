using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class BunchService : IBunchService
    {
        private readonly IBunchRepository _bunchRepository;

        public BunchService(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public Bunch Get(string slug)
        {
            return _bunchRepository.Get(slug);
        }

        public IList<SmallBunch> GetByUserId(string userName)
        {
            return _bunchRepository.SearchByUser(userName);
        }

        public IList<SmallBunch> GetList()
        {
            return _bunchRepository.Search();
        }

        public Bunch Add(Bunch bunch)
        {
            return _bunchRepository.Add(bunch);
        }

        public Bunch Save(Bunch bunch)
        {
            return _bunchRepository.Update(bunch);
        }
    }
}