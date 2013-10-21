using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Infrastructure.Plumbing;
using NUnit.Framework;
using Web.Plumbing;

namespace sr.se.unittest
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
                    
                };
        }

        [TestCase(typeof(WebObjectFactory))]
        [TestCase(typeof(InfrastructureObjectFactory))]
        public void Resolve_ApplicableInterfacesCanBeResolved(Type objectFactoryType)
        {
            var windsorContainer = new WindsorContainer().Install(FromAssembly.This());
            WebObjectFactory.RegisterTypes(windsorContainer);
            VerifyInterfaceResolve(windsorContainer, objectFactoryType, _ignoredInterfaces);
        }

        private static void VerifyInterfaceResolve(IWindsorContainer container, Type objectFactoryType, IList<Type> ignoredInterfaces)
        {
            var webSiteAssembly = Assembly.GetAssembly(objectFactoryType);
            var interfaces = webSiteAssembly.GetTypes().Where(x => x.IsInterface);
            foreach (var i in interfaces)
            {
                if (!ignoredInterfaces.Contains(i))
                {
                    container.Resolve(i);
                    //Assert.IsTrue(ObjectFactory.CanResolve(container, i), string.Format("Interface: {0} kan inte resolvas", i));
                }
            }
        }
    }
}