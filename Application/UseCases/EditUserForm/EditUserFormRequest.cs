namespace Application.UseCases.EditUserForm
{
    public class EditUserFormRequest
    {
        public string UserName { get; private set; }

        public EditUserFormRequest(string userName)
        {
            UserName = userName;
        }
    }
}