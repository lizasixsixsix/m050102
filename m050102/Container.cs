﻿using System;
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
                                        false)
                            && ((ExportAttribute)t.GetCustomAttributes(typeof(ExportAttribute), false)[0]).Contract == null)
                .ToList().ForEach(this.Register);

            assembly.GetTypes()
                .Where(t => t.IsDefined(typeof(ExportAttribute),
                                        false)
                            && ((ExportAttribute)t.GetCustomAttributes(typeof(ExportAttribute), false)[0]).Contract != null)
                .ToList().ForEach(t => this.RegisterAs(((ExportAttribute)t.GetCustomAttributes(typeof(ExportAttribute), false)[0]).Contract, t));
        }

        public void Register(Type type)
        {
            this.Registrants.Add(type, type);
        }

        public void RegisterAs(Type contract, Type actual)
        {
            this.Registrants.Add(contract, actual);
        }

        public T CreateInstance<T>()
        {
            T t;

            if (typeof(T).IsDefined(typeof(ImportCtorAttribute),
                                    false))
            {
                t = CreateInstanceWithCtorParams<T>();
            }
            else
            {
                t = (T)typeof(T).GetConstructor(Type.EmptyTypes)?.Invoke(null);
            }

            typeof(T).GetProperties().Where(
                    p => p.IsDefined(typeof(ImportAttribute),
                        false))
                .ToList().ForEach(p => p.SetValue(
                    t,
                    this.Realizations.Get(this.Registrants[p.PropertyType]),
                    null));

            return t;
        }

        private T CreateInstanceWithCtorParams<T>()
        {
            var firstParamCtor = typeof(T)
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .First(c => c.GetParameters().Any());

            var ctorParams = firstParamCtor.GetParameters()
                .Select(p => this.Realizations.Get(this.Registrants[p.ParameterType]))
                .ToArray();

            var t = (T)typeof(T).GetConstructor(
                ctorParams.Select(p => p.GetType()).ToArray())
                ?.Invoke(ctorParams);

            return t;
        }
    }
}
