using m050102.Attributes;

namespace m050102.TestAssembly
{
    public class Bar
    {
        [Import]
        public Foo Foo { get; set; }
    }
}
