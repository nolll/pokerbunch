namespace Core.UseCases.EditCashgameForm
{
    public class EditCashgameFormRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }

        public EditCashgameFormRequest(string slug, string dateStr)
        {
            Slug = slug;
            DateStr = dateStr;
        }
    }
}