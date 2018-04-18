using System;
using System.Collections.Generic;

namespace m050102
{
    public class Realizations : Dictionary<Type, object>
    {
        public object Get(Type type)
        {
            if (!this.ContainsKey(type))
            {
                this.Add(
                    type,
                    type.GetConstructor(Type.EmptyTypes)?.Invoke(null));
            }

            return this[type];
        }
    }
}
