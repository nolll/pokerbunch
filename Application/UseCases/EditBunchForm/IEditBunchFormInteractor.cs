namespace Application.UseCases.EditBunchForm
{
    public interface IEditBunchFormInteractor
    {
        EditBunchFormResult Execute(EditBunchFormRequest request);
    }
}