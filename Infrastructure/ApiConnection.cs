using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Core.Exceptions;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class ApiConnection
    {
        private readonly string _apiHost;
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly string _username;
        private readonly string _password;
        private static ApiConnection _instance;
        private string _token;

        private ApiConnection(string apiHost, string apiUrl, string apiKey, string username, string password)
        {
            _apiHost = apiHost;
            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _username = username;
            _password = password;
        }

        public static ApiConnection GetInstance(string apiHost, string apiUrl, string apiKey, string username, string password)
        {
            if (_instance == null)
                _instance = new ApiConnection(apiHost, apiUrl, apiKey, username, password);
            return _instance;
        }

        public T ReadObject<T>(string apiUrl)
        {
            var json = ReadJson(apiUrl);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private string ReadJson(string apiUrl, bool isRetry = false)
        {
            HttpStatusCode statusCode;
            using (var client = new BearerClient(_apiUrl, Token))
            {
                var response = client.GetAsync(apiUrl).Result;
                statusCode = response.StatusCode;
                if ((int)statusCode >= 200 && (int)statusCode < 300)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                throw new AccessDeniedException();
            }
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                if (isRetry)
                    throw new ApiException();
                RefreshToken();
                return ReadJson(apiUrl, true);
            }
            return string.Empty;
        }

        private string Token
        {
            get
            {
                if (_token == null)
                    RefreshToken();
                return _token;
            }
        }

        private void RefreshToken()
        {
            _token = SignIn();
        }

        private string SignIn()
        {
            using (var client = new SignInClient(_apiUrl))
            {
                var content = GetPostContentForSignIn();
                var response = client.PostAsync("token", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            return string.Empty;
        }

        private FormUrlEncodedContent GetPostContentForSignIn()
        {
            return new FormUrlEncodedContent(GetPostValuesForSignIn());
        }

        private KeyValuePair<string, string>[] GetPostValuesForSignIn()
        {
            return new[]
            {
                GetFormParam("grant_type", "password"),
                GetFormParam("client_id", _apiKey),
                GetFormParam("username", _username),
                GetFormParam("password", _password)
            };
        }

        private static KeyValuePair<string, string> GetFormParam(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
