using System.Collections.Generic;
using Application.Urls;

namespace Application.UseCases.InvitePlayer
{
    public class InvitePlayerResult
    {
        public bool Success { get; private set; }
        public IList<string> Errors { get; private set; }
        public Url ReturnUrl { get; private set; }

        public InvitePlayerResult(bool success, IList<string> errors, Url returnUrl)
        {
            Success = success;
            Errors = errors;
            ReturnUrl = returnUrl;
        }
    }
}