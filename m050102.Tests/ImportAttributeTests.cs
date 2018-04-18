using System;
using System.Reflection;
using m050102.TestAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace m050102.Tests
{
    [TestClass]
    public class ImportAttributeTests
    {
        [TestMethod]
        public void InjectProperty_PropertyInjected()
        {
            var container = new Container();

            var assembly = Assembly.Load(AssemblyReference.Name);

            container.Register(assembly);

            var bar = container.CreateInstance<Bar>();

            Assert.IsNotNull(bar.Foo);
        }
    }
}
