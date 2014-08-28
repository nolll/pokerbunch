using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Edit;

namespace Web.Commands.HomegameCommands
{
    public class EditBunchCommand : Command
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly Bunch _bunch;
        private readonly BunchEditPostModel _postModel;

        public EditBunchCommand(
            IBunchRepository bunchRepository,
            Bunch bunch, 
            BunchEditPostModel postModel)
        {
            _bunchRepository = bunchRepository;
            _bunch = bunch;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var postedHomegame = BunchModelMapper.GetBunch(_bunch, _postModel);
            _bunchRepository.Save(postedHomegame);
            return false;
        }
    }
}