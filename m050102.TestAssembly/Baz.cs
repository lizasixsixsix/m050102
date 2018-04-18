using System.Security.Cryptography.X509Certificates;
using m050102.Attributes;

namespace m050102.TestAssembly
{
    [ImportCtor]
    public class Baz
    {
        public Foo Foo { get; set; }

        public Baz(Foo foo)
        {
            this.Foo = foo;
        }
    }
}
