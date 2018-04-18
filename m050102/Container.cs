using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using m050102.Attributes;

namespace m050102
{
    public class Container
    {
        public Dictionary<Type, Type> Registrants { get; set; }

        public Realizations Realizations { get; set; }

        public Container()
        {
            this.Registrants = new Dictionary<Type, Type>();

            this.Realizations = new Realizations();
        }

        public void Register(Assembly assembly)
        {
            assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(ExportAttribute),
                                        false)
                                    .Any())
                    .ToList().ForEach(this.Register);
        }

        public void Register(Type type)
        {
            this.Registrants.Add(type, type);

            this.Realizations.Add(
                type,
                type.GetConstructor(Type.EmptyTypes)?.Invoke(null));
        }

        public void Register(params Type[] types)
        {
            foreach (var t in types)
            {
                this.Register(t);
            }
        }

        public T CreateInstance<T>()
            where T : new()
        {
            var t = new T();

            typeof(T).GetProperties().Where(
                p => p.GetCustomAttributes(typeof(ImportAttribute),
                    false).Any())
                .ToList().ForEach(p => p.SetValue(
                    t,
                    this.Realizations.Get(p.PropertyType),
                    null));

            return t;
        }
    }
}
