namespace RAPLNet.Attributes;

public interface IBenchmarkState
{
    public long LoopIterations { get; }
    public long Iterations { get; }
}
