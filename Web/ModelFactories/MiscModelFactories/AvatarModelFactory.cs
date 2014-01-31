using Application.Services;
using Core.Classes;
using Web.Models.MiscModels;

namespace Web.ModelFactories.MiscModelFactories{

	public class AvatarModelFactory : IAvatarModelFactory
	{
	    private readonly IAvatarService _avatarService;

	    public AvatarModelFactory(IAvatarService avatarService)
		{
		    _avatarService = avatarService;
		}
        
	    public AvatarModel Create(string email, AvatarSize size = AvatarSize.Large){
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
				return _avatarService.GetSmallAvatarUrl(email);
			}
			return _avatarService.GetLargeAvatarUrl(email);
		}

	}

}