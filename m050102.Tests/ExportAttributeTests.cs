using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using m050102.TestAssembly;

namespace m050102.Tests
{
    [TestClass]
    public class ExportAttributeTests
    {
        [TestMethod]
        public void LoadAssembly_ClassRegistered()
        {
            var container = new Container();

            var assembly = Assembly.Load(AssemblyReference.Name);

            container.Register(assembly);

            Assert.IsTrue(container.Registrants.ContainsKey(typeof(Foo)));
        }

        [TestMethod]
        public void LoadAssembly_ClassRegisteredAsInterface()
        {
            var container = new Container();

            var assembly = Assembly.Load(AssemblyReference.Name);

            container.Register(assembly);

            Assert.IsTrue(container.Registrants.ContainsKey(typeof(IQuux)));

            Assert.IsTrue(container.Registrants.ContainsValue(typeof(Qux)));
        }
    }
}
