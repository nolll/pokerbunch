﻿using Microsoft.Owin;
using Owin;
using Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}