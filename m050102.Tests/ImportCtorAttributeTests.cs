using System;
using System.Reflection;
using m050102.TestAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace m050102.Tests
{
    [TestClass]
    public class ImportCtorAttributeTests
    {
        [TestMethod]
        public void InjectCtorParams_CtorParamsInjected()
        {
            var container = new Container();

            var assembly = Assembly.Load(AssemblyReference.Name);

            container.Register(assembly);

            var baz = container.CreateInstance<Baz>();

            Assert.IsNotNull(baz.Foo);

            Assert.IsInstanceOfType(baz.Foo, typeof(Foo));
        }
    }
}
