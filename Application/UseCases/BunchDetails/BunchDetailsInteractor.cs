using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.BunchDetails
{
    public static class BunchDetailsInteractor
    {
        public static BunchDetailsResult Execute(IBunchRepository bunchRepository, IAuth auth, BunchDetailsRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var bunchName = bunch.DisplayName;
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var editBunchUrl = new EditBunchUrl(bunch.Slug);
            var canEdit = auth.IsInRole(bunch.Slug, Role.Manager);

            return new BunchDetailsResult(bunchName, description, houseRules, editBunchUrl, canEdit);
        }
    }
}