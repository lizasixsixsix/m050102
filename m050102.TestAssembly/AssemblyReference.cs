using System.Reflection;

namespace m050102.TestAssembly
{
    public static class AssemblyReference
    {
        public static AssemblyName Name
            =>
                Assembly.GetAssembly(typeof(AssemblyReference)).GetName();
    }
}