using Application.Services;
using Core.Entities;
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
			if(email == null)
                return new AvatarModel();
		    return new AvatarModel(GetAvatarUrl(email, size));
		}

		private string GetAvatarUrl(string email, AvatarSize size){
			if(size == AvatarSize.Small){
				return _avatarService.GetSmallAvatarUrl(email);
			}
			return _avatarService.GetLargeAvatarUrl(email);
		}

	}

}