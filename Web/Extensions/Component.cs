namespace Web.Extensions
{
    public abstract class Component
    {
        public virtual string ViewName => ComponentViewFinder.GetViewFor(GetType());
    }
}