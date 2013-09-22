using Core.Classes;

namespace Core.Services{
    public interface IInvitationCodeCreator{

		string GetCode(Player player);

	}

}