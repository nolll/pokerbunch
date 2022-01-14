using System;
using System.Security.Cryptography;
using System.Text;

namespace Web.InlineCode;

public abstract class InlineCodeHtml
{
    private readonly string _startTag;
    private readonly string _endTag;
    protected abstract string Content { get; }
    public string Html => $"{_startTag}{Content}{_endTag}";

    protected InlineCodeHtml(string startTag, string endTag)
    {
        _startTag = startTag;
        _endTag = endTag;
    }

    public string Hash
    {
        get
        {
            using var sha256Hash = SHA256.Create();
            return Convert.ToBase64String(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Content)));
        }
    }
}