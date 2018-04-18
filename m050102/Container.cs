using System;
using System.CodeDom;
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
                    .Where(t => t.IsDefined(typeof(ExportAttribute),
                                            false))
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
        {
            T t;

            if (typeof(T).IsDefined(typeof(ImportCtorAttribute),
                false))
            {
                t = (T)typeof(T).GetConstructor(Type.EmptyTypes)?.Invoke(null);
            }
            else
            {
                t = CreateInstanceWithCtorParams<T>();
            }

            typeof(T).GetProperties().Where(
                    p => p.IsDefined(typeof(ImportAttribute),
                        false))
                .ToList().ForEach(p => p.SetValue(
                    t,
                    this.Realizations.Get(p.PropertyType),
                    null));

            return t;
        }

        private T CreateInstanceWithCtorParams<T>()
        {
            var firstParamCtor = typeof(T)
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .First(c => c.GetParameters().Any());

            var ctorParams = firstParamCtor.GetParameters()
                .Select(p => this.Realizations.Get(p.GetType()))
                .ToArray();

            var t = (T)typeof(T).GetConstructor(
                ctorParams.Select(p => p.GetType()).ToArray())
                ?.Invoke(ctorParams);

            return t;
        }
    }
}
