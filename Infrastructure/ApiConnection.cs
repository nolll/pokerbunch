using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Core.Exceptions;
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

        public T Get<T>(string apiUrl)
        {
            var json = ReadJson(apiUrl);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T Post<T>(string apiUrl, object data)
        {
            var json = PostJson(apiUrl, data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private string ReadJson(string apiUrl)
        {
            using (var client = new BearerClient(_url, _token))
            {
                var response = client.GetAsync(apiUrl).Result;
                ValidateResponse(response);
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private string PostJson(string apiUrl, object data)
        {
            using (var client = new BearerClient(_url, _token))
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, content).Result;
                ValidateResponse(response);
                return response.Content.ReadAsStringAsync().Result;
            }
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
            if (statusCode == HttpStatusCode.BadRequest)
                throw new ValidationException(GetValidationErrorMessage(response));
            throw new Exception("Unknown error");
        }

        private string GetValidationErrorMessage(HttpResponseMessage response)
        {
            try
            {
                var jsonMessage = response.Content.ReadAsStringAsync().Result;
                var messageObj = JsonConvert.DeserializeObject<ValidationMessage>(jsonMessage);
                return messageObj.Message;
            }
            catch (Exception)
            {
                return "Validation error";
            }
        }

        private class ValidationMessage
        {
            public string Message { get; set; }
        }

        public string GetToken(string userName, string password)
        {
            return SignIn(userName, password);
        }

        private string SignIn(string userName, string password)
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
