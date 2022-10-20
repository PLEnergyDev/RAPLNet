namespace RAPLNet.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class BencharkIterationsAttribute : BenchParameterAttribute
{
    public BencharkIterationsAttribute() : base("Iterations")
    {
    }
}
