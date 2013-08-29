using Core.Classes;
using Core.Services;

namespace Web.Models.PlayerModels.Details{

	class AvatarModelBuilder : IAvatarModelBuilder
	{
	    private readonly IAvatarService _avatarService;

	    public AvatarModelBuilder(IAvatarService avatarService)
		{
		    _avatarService = avatarService;
		}
        
	    public AvatarModel Build(string email, AvatarSize size = AvatarSize.Large){
			var model = new AvatarModel();
			if(email != null){
				model.AvatarEnabled = true;
                model.AvatarUrl = GetAvatarUrl(email, size);
			} else {
				model.AvatarEnabled = false;
			}
			return model;
		}

		private string GetAvatarUrl(string email, AvatarSize size){
			if(size == AvatarSize.Small){
				return _avatarService.getSmallAvatarUrl(email);
			}
			return _avatarService.getLargeAvatarUrl(email);
		}

	}

}