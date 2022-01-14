using JetBrains.Annotations;

namespace Web.Extensions;

public class View
{
    private string ViewName { get; }

    public View([AspMvcView] string viewName)
    {
        ViewName = viewName;
    }

    public static implicit operator string(View view)
    {
        return view?.ViewName;
    }
}