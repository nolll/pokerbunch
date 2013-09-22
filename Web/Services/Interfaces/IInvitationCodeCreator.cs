using Core.Classes;

namespace Web.Services.Interfaces{
    public interface IInvitationCodeCreator{

		string GetCode(Player player);

	}

}