namespace Application.UseCases.UserDetails
{
    public interface IUserDetailsInteractor
    {
        UserDetailsResult Execute(UserDetailsRequest request);
    }
}