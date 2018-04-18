using System;

namespace m050102.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        { }

        public ExportAttribute(Type contract)
        {
            Contract = contract;
        }

        public Type Contract { get; }
    }
}
