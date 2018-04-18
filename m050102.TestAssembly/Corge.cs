using m050102.Attributes;

namespace m050102.TestAssembly
{
    public class Corge
    {
        [Import]
        public IQuux Quux { get; set; }
    }
}
