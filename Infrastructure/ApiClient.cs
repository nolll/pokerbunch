﻿using System.Net.Http;
using System.Net.Http.Headers;

namespace Infrastructure
{
    public class ApiClient : HttpClient
    {
        protected ApiClient()
        {
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
