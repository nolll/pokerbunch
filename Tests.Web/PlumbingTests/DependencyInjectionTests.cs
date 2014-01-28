using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Classes;
using NUnit.Framework;
using Plumbing;
using Web.Models.PageBaseModels;
using Web.Plumbing;
using DependencyResolver = Plumbing.DependencyResolver;

namespace Tests.Web.PlumbingTests
{
    [TestFixture]
    public class DependencyInjectionTests
    {
        private readonly IList<Type> _ignoredInterfaces;

        public DependencyInjectionTests()
        {
            // Interface som inte är injicerade och som inte ska injiceras
            _ignoredInterfaces = new List<Type>
                {
                    typeof(IPageModel),
                    typeof(ICacheable)
                };
        }

        [Test]
        public void Resolve_ApplicableInterfacesCanBeResolved()
        {
            var webAssembly = Assembly.GetAssembly(typeof(WebDependencyResolver));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureDependencyResolver));

            var windsorContainer = new WindsorContainer()
                .Install(FromAssembly.Instance(infrastructureAssembly))
                .Install(FromAssembly.Instance(webAssembly));
            var dependencyResolver = new WebDependencyResolver(windsorContainer, LifestyleType.Transient);
            VerifyInterfaceResolve(dependencyResolver, webAssembly, _ignoredInterfaces);
            VerifyInterfaceResolve(dependencyResolver, infrastructureAssembly, _ignoredInterfaces);
        }

        private static void VerifyInterfaceResolve(DependencyResolver dependencyResolver, Assembly assembly, IList<Type> ignoredInterfaces)
        {
            var interfaces = assembly.GetTypes().Where(x => x.IsInterface);
            foreach (var i in interfaces)
            {
                if (!ignoredInterfaces.Contains(i))
                {
                    dependencyResolver.ResolveOrThrow(i);
                }
            }
        }
    }
}