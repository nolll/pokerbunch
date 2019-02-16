using System;
using System.Net.Http;

namespace PokerBunch.Client.Connection
{
    public class ApiException : Exception
    {
        private readonly string _url;
        private readonly string _errorCode;
        private readonly string _errorMessage;

        public ApiException(HttpResponseMessage response)
        {
            _url = response.RequestMessage.RequestUri.ToString();
            _errorCode = response.StatusCode.ToString();
            _errorMessage = ApiErrorMessage.Get(response);
        }

        public override string Message
        {
            get
            {
                var message = $"Api Error: {_errorCode}. Url: {_url}";
                if (_errorMessage == null)
                    return message;
                return string.Concat(message, $". Message: {_errorMessage}");
            }
        }
    }
}