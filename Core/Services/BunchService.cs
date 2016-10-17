using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
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

        public IList<Bunch> GetByUserId(string userId)
        {
            var ids = _bunchRepository.SearchByUser(userId);
            return _bunchRepository.Get(ids);
        }

        public IList<Bunch> GetList()
        {
            var ids = _bunchRepository.Search();
            return _bunchRepository.Get(ids);
        }

        public string Add(Bunch bunch)
        {
            return _bunchRepository.Add(bunch);
        }

        public void Save(Bunch bunch)
        {
            _bunchRepository.Update(bunch);
        }
    }
}