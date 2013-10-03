namespace Core.Services{

	public interface ISocialServiceProvider {

		ISocialService Get(string identifier);

	}

}