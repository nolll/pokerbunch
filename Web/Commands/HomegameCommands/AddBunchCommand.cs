using System;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Web.Models.HomegameModels.Add;

namespace Web.Commands.HomegameCommands
{
    public class AddBunchCommand : Command
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly AddBunchPostModel _postModel;

        public AddBunchCommand(
            IBunchRepository bunchRepository,
            IAuth auth,
            IPlayerRepository playerRepository,
            AddBunchPostModel postModel)
        {
            _bunchRepository = bunchRepository;
            _auth = auth;
            _playerRepository = playerRepository;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            if (BunchExists())
            {
                AddError("The Bunch name is not available");
                return false;
            }
            var bunch = CreateBunch(_postModel);
            bunch = _bunchRepository.Add(bunch);
            var user = _auth.CurrentUser;
            _playerRepository.Add(bunch, user, Role.Manager);
            return true;
        }

        private bool BunchExists()
        {
            var slug = SlugGenerator.GetSlug(_postModel.DisplayName);
            var bunch = _bunchRepository.GetBySlug(slug);
            return bunch != null;
        }

        private static Bunch CreateBunch(AddBunchPostModel postModel)
        {
            return new Bunch(
                    0,
                    SlugGenerator.GetSlug(postModel.DisplayName),
                    postModel.DisplayName,
                    postModel.Description,
                    string.Empty,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    200,
                    new Currency(postModel.CurrencySymbol, postModel.CurrencyLayout));
        }
    }
}