using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using m050102.TestAssembly;

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

            Assert.IsInstanceOfType(bar.Foo, typeof(Foo));
        }

        [TestMethod]
        public void InjectPropertyAsInterface_PropertyInjectedWithClass()
        {
            var container = new Container();

            var assembly = Assembly.Load(AssemblyReference.Name);

            container.Register(assembly);

            var corge = container.CreateInstance<Corge>();

            Assert.IsNotNull(corge.Quux);

            Assert.IsInstanceOfType(corge.Quux, typeof(Qux));
        }
    }
}
