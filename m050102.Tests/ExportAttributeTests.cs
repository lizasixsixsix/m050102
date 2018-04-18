using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using m050102.TestAssembly;

namespace m050102.Tests
{
    [TestClass]
    public class ExportAttributeTests
    {
        [TestMethod]
        public void LoadAssembly_ContainerContainsMethodWithExportAttribute()
        {
            var container = new Container();

            var assembly = Assembly.Load(AssemblyReference.Name);

            container.Register(assembly);

            Assert.IsNotNull(container.Registrants);
        }
    }
}
