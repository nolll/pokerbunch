﻿using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Infrastructure.Plumbing;
using Web.Plumbing;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ObjectFactory _objectFactory;

        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BootstrapContainer();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnsureLowercaseUrl();
        }

        private void EnsureLowercaseUrl()
        {
            // Don't rewrite requests for content (.png, .css) or scripts (.js)
            if (Request.Url.AbsolutePath.Contains("/Frontend/"))
                return;

            // If uppercase chars exist, redirect to a lowercase version
            var url = Request.Url.ToString();
            if (Regex.IsMatch(url, @"[A-Z]"))
            {
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                Response.AddHeader("Location", url.ToLower());
                Response.End();
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterRoutes(routes);
        }

        private static void BootstrapContainer()
        {
            var windsorContainer = new WindsorContainer().Install(FromAssembly.This());
            _objectFactory = new WebObjectFactory(windsorContainer);
            var controllerFactory = new WindsorControllerFactory(windsorContainer.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            _objectFactory.Dispose();
        }
    }
}