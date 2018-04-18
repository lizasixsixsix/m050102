using m050102.Attributes;

namespace m050102.TestAssembly
{
    [Export(typeof(IQuux))]
    public class Qux : IQuux
    {
    }
}
