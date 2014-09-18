namespace Application.Services
{
    public class RandomService : IRandomService
    {
        public string GetPasswordCharacters()
        {
            return "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
        }

        public string GetSaltCharacters()
        {
            throw new System.NotImplementedException();
        }
    }
}