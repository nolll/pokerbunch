using Core.UseCases.CopyToRaven;

namespace Web.Models.AdminModels
{
    public class CopyToRavenModel
    {
        public int UserCount { get; private set; }

        public CopyToRavenModel(CopyToRavenOutput output)
        {
            UserCount = output.UsersCopied;
        }
    }
}