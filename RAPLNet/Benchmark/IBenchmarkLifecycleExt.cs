using RAPLNet.Attributes;
using RAPLNet.Benchmark;

using System.Reflection;

public static class IBenchmarkLifecycleExt
{
    public static object[] GetParameters(this IBenchmarkLifecycle lf)
    {

        ParameterInfo[] vs = lf.Method.GetParameters();
        var paramvalues = new object[vs.Length];
        foreach (var v in vs)
        {
            paramvalues[v.Position] = v.GetCustomAttribute<BenchParameterAttribute>()?.BenchmarkParameterSource switch
            {
                "LoopIterations" => lf.BenchmarkInfo.LoopIterations,
                "Iterations" => lf.BenchmarkInfo.Iterations,
                null => throw new NotSupportedException($"Unmarked parameter: [{v.Name}] position:[{v.Position}] of method: [{lf.Method.Name}] -- mark with {nameof(BenchParameterAttribute)}"),
                string parameterName => throw new InvalidOperationException($"Unknown parameter: [{parameterName}] position:[{v.Position}] of method: [{lf.Method.Name}]")
            };
        }
        return paramvalues;
    }
}