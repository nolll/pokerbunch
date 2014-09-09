using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Services;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Web.Plumbing;
using DependencyResolver = Plumbing.DependencyResolver;

namespace Tests.Web.PlumbingTests
{
    [TestFixture]
    public class DependencyInjectionTests : TestBase
    {
        private readonly IList<Type> _ignoredInterfaces;

        public DependencyInjectionTests()
        {
            // Interface som inte är injicerade och som inte ska injiceras
            _ignoredInterfaces = new List<Type>
                {
                    typeof(ICacheable),
                    typeof(ISocialService)
                };
        }

        [Test]
        public void Resolve_ApplicableInterfacesCanBeResolved()
        {
            var assemblyNames = new List<string>
                {
                    "PokerBunch.Core",
                    "Application",
                    "Web"
                };

            var assemblies = assemblyNames.Select(Assembly.Load).ToList();
            var windsorContainer = new WindsorContainer();
            var dependencyResolver = new WebDependencyResolver(windsorContainer, LifestyleType.Transient);

            foreach (var assembly in assemblies)
            {
                windsorContainer.Install(FromAssembly.Instance(assembly));
                VerifyInterfaceResolve(dependencyResolver, assembly, _ignoredInterfaces);
            }
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