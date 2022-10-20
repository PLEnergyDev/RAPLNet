namespace RAPLNet.Attributes;

public record BenchmarkState(long LoopIterations, long Iterations) : IBenchmarkState;
