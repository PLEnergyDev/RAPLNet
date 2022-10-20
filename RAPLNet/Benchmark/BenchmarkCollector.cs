using System;
using System.Reflection;

using RAPLNet.Attributes;

namespace RAPLNet.Benchmark;


public class BenchmarkCollector
{
    public long? DefaultIterations { get; set; }
    public long? DefaultLoopIterations { get; set; }

    public List<IBenchmarkLifecycle> CollectBenchmarks(Assembly assembly)
    {
        List<IBenchmarkLifecycle> collectedBenchmarks = new List<IBenchmarkLifecycle>();

        
        IEnumerable<MethodInfo> methods = assembly.GetTypes().SelectMany(type => type
            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(info => info.GetCustomAttribute<BenchmarkAttribute>() != null));

        foreach (MethodInfo benchmarkMethod in methods)
        {
            var benchmarkAttribute = benchmarkMethod.GetCustomAttribute<BenchmarkAttribute>()!;
            if (benchmarkAttribute.Skip ||
                benchmarkMethod.DeclaringType!.GetCustomAttribute<SkipBenchmarksAttribute>() != null)
            {
                continue;
            }
            collectedBenchmarks.Add(CreateBenchmark(benchmarkMethod, benchmarkAttribute));
        }
        return collectedBenchmarks;
    }
    public IBenchmarkLifecycle CreateBenchmark(MethodInfo benchmarkMethod, BenchmarkAttribute benchmarkAttribute)
    {
        var bi = new BenchmarkInfo(
            benchmarkAttribute.Name ?? benchmarkMethod.Name,
            benchmarkAttribute.Group!,
            benchmarkAttribute.Order,
            benchmarkAttribute.Iterations ?? DefaultIterations ,
            benchmarkAttribute.Loopiterations??DefaultLoopIterations
            )
        {
        };

        var ctors = benchmarkAttribute.BenchmarkLifecycleClass.GetConstructors()
            .Select(
                v => new {
                    constructorParamTypes = v.GetParameters()
                    .Select(v => v.ParameterType)
                    .ToArray(),
                    ctor = v
                })
            .Where(v => v.constructorParamTypes.Count() <= 2)
            .OrderByDescending(v => v.constructorParamTypes.Count())
            .ToList();
        IBenchmarkLifecycle? lifecycle = null;
        List<object> args = new List<object>();
        foreach (var v in ctors)
        {
            foreach (var ctorParamType in v.constructorParamTypes)
            {
                if (ctorParamType.IsAssignableFrom(bi.GetType()))
                {
                    args.Add(bi);
                }
                else if (ctorParamType.IsAssignableFrom(benchmarkMethod.GetType()))
                {
                    args.Add(benchmarkMethod);
                }
                else
                {
                    goto continueOuter;
                }
            }
            lifecycle = (IBenchmarkLifecycle)v.ctor.Invoke(args.ToArray());
        continueOuter:
            args.Clear();
            continue;
        }
        
        return (lifecycle ?? throw new InvalidOperationException("Unsatisfiable lifecycle constructors"));
    }
}
