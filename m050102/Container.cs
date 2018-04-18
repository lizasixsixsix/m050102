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
            this.Registrants = new HashSet<Type>();
        }

        public void Register(Assembly assembly)
        {
            this.Registrants = new HashSet<Type>(
                assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(ExportAttribute),
                                        false)
                                    .Any()));
        }

        public void Register(Type type)
        {
            this.Registrants.Add(type);
        }

        public void Register(params Type[] types)
        {
            this.Registrants.UnionWith(types);
        }
    }
}
