using Core.Classes;

namespace Application.Services.Interfaces{
    public interface IInvitationCodeCreator{

		string GetCode(Player player);

	}

}