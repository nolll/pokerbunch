namespace Web.Extensions
{
    public abstract class Component
    {
        public virtual string ViewName
        {
            get
            {
                return ComponentViewFinder.GetViewFor(GetType());
            }
        }
    }
}