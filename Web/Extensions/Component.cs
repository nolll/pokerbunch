using Web.Models;

namespace Web.Extensions
{
    public abstract class Component : IViewModel
    {
        public View GetView()
        {
            return new View(ComponentViewFinder.GetViewFor(GetType()));
        }
    }
}