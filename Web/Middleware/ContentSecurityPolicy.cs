using System.Collections.Generic;
using System.Linq;

namespace Web.Middleware;

public class ContentSecurityPolicy
{
    private readonly List<ContentSecurityDirective> _directives;

    public ContentSecurityPolicy()
    {
        _directives = new List<ContentSecurityDirective>();
    }

    public ContentSecurityPolicy AddDirective(ContentSecurityDirective directive)
    {
        _directives.Add(directive);
        return this;
    }

    public override string ToString()
    {
        return string.Join("; ", _directives.Select(o => o.ToString()));
    }
}