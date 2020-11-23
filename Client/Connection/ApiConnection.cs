using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Core.Services;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PokerBunch.Client.Exceptions;
using PokerBunch.Common.Urls;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Connection
{
    public class ApiConnection
    {
        private readonly string _key;
        private readonly bool _enableDetailedErrors;
        private readonly IUrlFormatter _urlFormatter;
        private readonly ITokenReader _tokenReader;

        public ApiConnection(string key, bool enableDetailedErrors, IUrlFormatter urlFormatter, ITokenReader tokenReader)
        {
            _key = key;
            _enableDetailedErrors = enableDetailedErrors;
            _urlFormatter = urlFormatter;
            _tokenReader = tokenReader;
        }

        public T Get<T>(ApiUrl apiUrl)
        {
            var json = ReadJson(apiUrl);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T Get<T>(string token, ApiUrl apiUrl)
        {
            var json = ReadJson(token, apiUrl);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Post(ApiUrl apiUrl, object data = null)
        {
            PostJson(apiUrl, data);
        }

        public T Post<T>(ApiUrl apiUrl, object data = null)
        {
            var json = PostJson(apiUrl, data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T Put<T>(ApiUrl apiUrl, object data)
        {
            var json = PutJson(apiUrl, data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private string ReadJson(ApiUrl apiUrl)
        {
            using (var client = GetClient())
            {
                var response = client.GetAsync(_urlFormatter.ToAbsolute(apiUrl)).Result;
                return ReadResponse(response);
            }
        }

        private string ReadJson(string token, ApiUrl apiUrl)
        {
            using (var client = GetClient(token))
            {
                var response = client.GetAsync(_urlFormatter.ToAbsolute(apiUrl)).Result;
                return ReadResponse(response);
            }
        }

        private string PostJson(ApiUrl apiUrl, object data)
        {
            using (var client = GetClient())
            {
                var content = GetJsonContent(data);
                var response = client.PostAsync(_urlFormatter.ToAbsolute(apiUrl), content).Result;
                return ReadResponse(response);
            }
        }

        private string PutJson(ApiUrl apiUrl, object data)
        {
            using (var client = GetClient())
            {
                var content = GetJsonContent(data);
                var response = client.PutAsync(_urlFormatter.ToAbsolute(apiUrl), content).Result;
                return ReadResponse(response);
            }
        }

        private BearerHttpClient GetClient() => new BearerHttpClient(_tokenReader.Read());
        private BearerHttpClient GetClient(string token) => new BearerHttpClient(token);

        private string ReadResponse(HttpResponseMessage response)
        {
            ValidateResponse(response);
            return response.Content.ReadAsStringAsync().Result;
        }

        private static StringContent GetJsonContent(object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }

        private void ValidateResponse(HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;
            if ((int) statusCode >= 200 && (int) statusCode < 300)
                return;
            if (_enableDetailedErrors)
                throw new ApiException(response);
            if (statusCode == HttpStatusCode.Forbidden)
                throw new AccessDeniedException();
            if (statusCode == HttpStatusCode.Unauthorized)
                throw new NotLoggedInException();
            if (statusCode == HttpStatusCode.NotFound)
                throw new NotFoundException(ApiErrorMessage.Get(response, "Not found"));
            if (statusCode == HttpStatusCode.BadRequest)
                throw new ValidationException(ApiErrorMessage.Get(response, "Validation error"));
            throw new Exception("Unknown error");
        }

        [UsedImplicitly]
        public class ResponseMessage
        {
            [UsedImplicitly]
            public string Message { get; set; }
        }
        
        public string SignIn(string userName, string password)
        {
            using (var client = new SignInHttpClient())
            {
                var content = GetFormContentForSignIn(userName, password);
                var signInUrl = new ApiTokenUrl();
                var response = client.PostAsync(_urlFormatter.ToAbsolute(signInUrl), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            return string.Empty;
        }

        private FormUrlEncodedContent GetFormContentForSignIn(string userName, string password)
        {
            return new FormUrlEncodedContent(GetFormValuesForSignIn(userName, password));
        }

        private IEnumerable<KeyValuePair<string, string>> GetFormValuesForSignIn(string userName, string password) => new[]
        {
            GetFormParam("grant_type", "password"),
            GetFormParam("client_id", _key),
            GetFormParam("username", userName),
            GetFormParam("password", password)
        };

        private static KeyValuePair<string, string> GetFormParam(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
