namespace Application.UseCases.EditUserForm
{
    public interface IEditUserFormInteractor
    {
        EditUserFormResult Execute(EditUserFormRequest request);
    }
}