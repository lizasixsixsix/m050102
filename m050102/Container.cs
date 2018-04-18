using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using m050102.Attributes;

namespace m050102
{
    public class Container
    {
        public HashSet<Type> Registrants { get; set; }

        public Container()
        {
        }

        public Container(params Assembly[] assemblies)
        {
            foreach (var a in assemblies)
            {
                this.RegisterAssembly(a);
            }
        }

        private void RegisterAssembly(Assembly assembly)
        {
            this.Registrants = new HashSet<Type>(
                assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(ExportAttribute),
                                        false)
                                    .Any()));
        }
    }
}
