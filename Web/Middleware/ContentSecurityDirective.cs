using System.Collections.Generic;

namespace Web.Middleware;

public class ContentSecurityDirective
{
    private readonly string _name;
    private readonly List<string> _values;

    public ContentSecurityDirective(string name)
    {
        _name = name;
        _values = new List<string>();
    }

    public ContentSecurityDirective AddSelf()
    {
        _values.Add("'self'");
        return this;
    }

    public ContentSecurityDirective AddDomain(string domain)
    {
        _values.Add(domain);
        return this;
    }

    public ContentSecurityDirective AddHash(string hash)
    {
        _values.Add($"'sha256-{hash}'");
        return this;
    }

    public override string ToString()
    {
        var values = string.Join(" ", _values);
        return $"{_name} {values}";
    }
}