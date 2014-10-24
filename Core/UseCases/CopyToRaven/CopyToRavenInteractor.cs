using Core.Repositories;

namespace Core.UseCases.CopyToRaven
{
    public class CopyToRavenInteractor
    {
        public static CopyToRavenOutput Execute(IUserRepository userRepository, IRavenUserRepository ravenUserRepository)
        {
            var users = userRepository.GetList();
            var usersCopied = users.Count;
            
            ravenUserRepository.Save(users);

            return new CopyToRavenOutput(usersCopied);
        }
    }

    public class CopyToRavenOutput
    {
        public int UsersCopied { get; private set; }

        public CopyToRavenOutput(int usersCopied)
        {
            UsersCopied = usersCopied;
        }
    }
}
