using RAPLNet.Benchmark;

namespace RAPLNet.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class BenchmarkAttribute : Attribute
{
    public string? Name { get; }

    public string? Group { get; }

    public string Description { get; }
    public int Order { get; }

    public bool Skip { get; }

    public int PlotOrder { get; }
    public long? Iterations { get; }
    public long? Loopiterations { get; }
    public Type BenchmarkLifecycleClass { get; }
    public BenchmarkAttribute(string? group, string description, Type? benchmarkLifecycleClass = null, int order = 0, bool skip = false,
        string? name = null, int plotOrder = 0, long iterations=-1, long loopiterations=-1)
    {
        if (benchmarkLifecycleClass != null && !benchmarkLifecycleClass.IsAssignableTo(typeof(IBenchmarkLifecycle)))
            throw new InvalidOperationException(benchmarkLifecycleClass.Name + " is not of type " + typeof(IBenchmarkLifecycle));

        BenchmarkLifecycleClass = benchmarkLifecycleClass ?? typeof(NopBenchmarkLifecycle);
        Group = group;
        Description = description;
        Order = order;
        PlotOrder = plotOrder;
        Iterations = iterations == -1 ? null : iterations;
        Loopiterations = loopiterations == -1 ? null : loopiterations;
        Skip = skip;
        Name = name;
    }
}
