namespace RAPLNet.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class BenchmarkLoopiterationsAttribute : BenchParameterAttribute
{
    public BenchmarkLoopiterationsAttribute() : base("LoopIterations")
    {
    }
}
