using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.BunchDetails
{
    public class BunchDetailsInteractor : BaseInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;

        public BunchDetailsInteractor(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository, IAuth auth)
            : base(bunchRepository, userRepository, playerRepository)
        {
            _bunchRepository = bunchRepository;
            _auth = auth;
        }

        public BunchDetailsResult Execute(BunchDetailsRequest request)
        {
            if(!HasPlayerAccess(request.Slug, request.UserName))
                throw new AccessDeniedException();

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            
            var bunchName = bunch.DisplayName;
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var editBunchUrl = new EditBunchUrl(bunch.Slug);
            var canEdit = _auth.IsInRole(bunch.Slug, Role.Manager);

            return new BunchDetailsResult(bunchName, description, houseRules, editBunchUrl, canEdit);
        }
    }
}