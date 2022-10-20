using System.Reflection;
using RAPLNet.Benchmark;
namespace RAPLNet.Attributes;

public class NopBenchmarkLifecycle : IBenchmarkLifecycle<IBenchmarkState>
{
    object? instance;
    public NopBenchmarkLifecycle(BenchmarkInfo bm, MethodInfo benchmarkedMethod)
    {
        Method = benchmarkedMethod;
        instance = Method.IsStatic ? null : Activator.CreateInstance(Method.DeclaringType!);
        BenchmarkInfo = bm;
    }
    public MethodInfo Method { get; }

    public BenchmarkInfo BenchmarkInfo { get; }
    public IBenchmarkState Initialize(BenchmarkInfo benchmark) => new BenchmarkState ( Iterations : benchmark.Iterations ?? 1, LoopIterations : benchmark.LoopIterations ?? 1 );
    public IBenchmarkState PostRun(IBenchmarkState oldstate) => oldstate;
    public IBenchmarkState PreRun(IBenchmarkState oldstate) => oldstate;

    public object Run(IBenchmarkState state)
    {
        var parameters = this.GetParameters();
        Method.Invoke(instance, parameters);
        return state;
    }

    public IBenchmarkState WarmupIteration(IBenchmarkState oldstate) => oldstate;
}
