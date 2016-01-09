using System;
using System.Web;

namespace Web 
{ 
    public class CustomServerHeaderModule : IHttpModule 
    { 
        public void Init(HttpApplication context) 
        { 
            context.PreSendRequestHeaders += OnPreSendRequestHeaders; 
        } 

        public void Dispose() 
        { } 

        void OnPreSendRequestHeaders(object sender, EventArgs e) 
        {
            if(HttpContext.Current != null)
                HttpContext.Current.Response.Headers.Remove("Server"); 
        } 
    } 
}