namespace Application.Services{

	public interface ISocialServiceProvider {

		ISocialService Get(string identifier);

	}

}