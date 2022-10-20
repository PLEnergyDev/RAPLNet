namespace RAPLNet.Benchmark;

public interface IBenchmarkState
{
    public ulong LoopIterations { get; set; }
    public ulong Iterations { get; set; }
}
