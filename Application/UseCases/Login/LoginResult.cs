using System.Collections.Generic;
using Application.Urls;

namespace Application.UseCases.Login
{
    public class LoginResult
    {
        public bool Success { get; private set; }
        public IList<string> Errors { get; private set; }
        public Url ReturnUrl { get; private set; }

        public LoginResult(bool success, IList<string> errors, Url returnUrl)
        {
            Success = success;
            Errors = errors;
            ReturnUrl = returnUrl;
        }
    }
}