using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Infrastructure
{
    public static class ApiErrorMessage
    {
        public static string Get(HttpResponseMessage response)
        {
            try
            {
                var jsonMessage = response.Content.ReadAsStringAsync().Result;
                var messageObj = JsonConvert.DeserializeObject<ApiConnection.ResponseMessage>(jsonMessage);
                return messageObj.Message;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Get(HttpResponseMessage response, string defaultMessage)
        {
            var errorMessage = Get(response);
            return errorMessage ?? defaultMessage;
        }
    }
}