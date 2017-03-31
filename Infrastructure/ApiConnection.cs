using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Core.Exceptions;
using Infrastructure.ApiUrls;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class ApiConnection
    {
        private readonly string _url;
        private readonly string _key;
        private readonly string _token;

        public ApiConnection(string url, string key, string token)
        {
            _url = url;
            _key = key;
            _token = token;
        }

        public T Get<T>(ApiUrl apiUrl)
        {
            var json = ReadJson(apiUrl.Url);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T Get<T>(string token, ApiUrl apiUrl)
        {
            var json = ReadJson(token, apiUrl.Url);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Post(ApiUrl apiUrl, object data = null)
        {
            PostJson(apiUrl.Url, data);
        }

        public T Post<T>(ApiUrl apiUrl, object data = null)
        {
            var json = PostJson(apiUrl.Url, data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T Put<T>(ApiUrl apiUrl, object data)
        {
            var json = PutJson(apiUrl.Url, data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public bool Delete(ApiUrl apiUrl)
        {
            DeleteJson(apiUrl.Url);
            return true;
        }

        private string ReadJson(string apiUrl)
        {
            using (var client = GetClient())
            {
                var response = client.GetAsync(apiUrl).Result;
                return ReadResponse(response);
            }
        }

        private string ReadJson(string token, string apiUrl)
        {
            using (var client = GetClient(token))
            {
                var response = client.GetAsync(apiUrl).Result;
                return ReadResponse(response);
            }
        }

        private string PostJson(string apiUrl, object data)
        {
            using (var client = GetClient())
            {
                var content = GetJsonContent(data);
                var response = client.PostAsync(apiUrl, content).Result;
                return ReadResponse(response);
            }
        }

        private string PutJson(string apiUrl, object data)
        {
            using (var client = GetClient())
            {
                var content = GetJsonContent(data);
                var response = client.PutAsync(apiUrl, content).Result;
                return ReadResponse(response);
            }
        }

        private string DeleteJson(string apiUrl)
        {
            using (var client = GetClient())
            {
                var response = client.DeleteAsync(apiUrl).Result;
                return ReadResponse(response);
            }
        }

        private BearerClient GetClient() => new BearerClient(_url, _token);
        private BearerClient GetClient(string token) => new BearerClient(_url, token);

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
            if (statusCode == HttpStatusCode.Forbidden)
                throw new AccessDeniedException();
            if (statusCode == HttpStatusCode.Unauthorized)
                throw new NotLoggedInException();
            if (statusCode == HttpStatusCode.NotFound)
                throw new NotFoundException(GetErrorMessage(response, "Not found"));
            if (statusCode == HttpStatusCode.BadRequest)
                throw new ValidationException(GetErrorMessage(response, "Validation error"));
            throw new Exception("Unknown error");
        }

        private string GetErrorMessage(HttpResponseMessage response, string defaultMessage)
        {
            try
            {
                var jsonMessage = response.Content.ReadAsStringAsync().Result;
                var messageObj = JsonConvert.DeserializeObject<ResponseMessage>(jsonMessage);
                return messageObj.Message;
            }
            catch (Exception)
            {
                return defaultMessage;
            }
        }

        [UsedImplicitly]
        private class ResponseMessage
        {
            [UsedImplicitly]
            public string Message { get; set; }
        }
        
        public string SignIn(string userName, string password)
        {
            using (var client = new SignInClient(_url))
            {
                var content = GetFormContentForSignIn(userName, password);
                var response = client.PostAsync("token", content).Result;
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
