namespace RAPLNet.Benchmark;

class NoGCBenchmarkRunner : IBenchmarkRunner
{
    IBenchMeter Meter;

    public long DefaultIterations { get; set; } = 10;
    public long MemorySize { get; set; } = 250_000_000;
    public void Run(IBenchmarkLifecycle lf)
    {
        Console.WriteLine($"BenchmarkLifecycle: {lf?.GetType().FullName}");
        Console.WriteLine("Initializing benchmark");
        var state = lf.Initialize(lf.BenchmarkInfo);
        Console.WriteLine("Warmup");
        for (var i = lf.BenchmarkInfo.Iterations??DefaultIterations; i>0 ; i--)
            state = lf.WarmupIteration(state);
        Console.WriteLine("Warmup done");

        Console.WriteLine("REInitializing benchmark");
        state = lf.Initialize(lf.BenchmarkInfo);

        GC.Collect();
        GC.TryStartNoGCRegion(MemorySize, false);

        Console.WriteLine("Preparing");
        state = lf.PreRun(state);

        Console.WriteLine("Running");

        Meter.Start();
        for (var i = lf.BenchmarkInfo.Iterations?? DefaultIterations; i > 0; i--)
            state = lf.Run(state);
        Meter.End();

        GC.EndNoGCRegion();

        state = lf.PostRun(state);
}
}