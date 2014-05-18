using Core.Entities;

namespace Application.Services{
    public interface IInvitationCodeCreator{

		string GetCode(Player player);

	}

}