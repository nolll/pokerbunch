using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;
using Core.Entities;
using Core.Services.Interfaces;
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
                    typeof(ISocialService),
                    typeof(IMessage)
                };
        }

        [Test]
        public void Resolve_ApplicableInterfacesCanBeResolved()
        {
            var assemblyNames = new List<string>
                {
                    "PokerBunch.Core",
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