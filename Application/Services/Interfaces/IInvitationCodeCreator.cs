using Core.Classes;

namespace Application.Services{
    public interface IInvitationCodeCreator{

		string GetCode(Player player);

	}

}