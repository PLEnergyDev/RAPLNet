using System.Reflection;

namespace RAPLNet.Benchmark;

public interface IBenchmarkLifecycle<T> : IBenchmarkLifecycle
{
    Type IBenchmarkLifecycle.Type => typeof(T);
    object IBenchmarkLifecycle.Initialize(BenchmarkInfo benchmark) => Initialize(benchmark);
    object IBenchmarkLifecycle.WarmupIteration(object oldstate) => WarmupIteration((T)oldstate);
    object IBenchmarkLifecycle.PreRun(object oldstate) => PreRun((T)oldstate);
    object IBenchmarkLifecycle.PostRun(object oldstate) => PostRun((T)oldstate);
    object IBenchmarkLifecycle.Run(object state) => Run((T)state);
    public new T Initialize(BenchmarkInfo benchmark); //TODO: should be fine.... create tests 
    public T WarmupIteration(T oldstate);
    public T PreRun(T oldstate);
    public object Run(T state);
    public T PostRun(T oldstate);
}

public interface IBenchmarkLifecycle
{
    public MethodInfo Method { get; }
    public Type Type { get; }
    public BenchmarkInfo BenchmarkInfo { get; }
    public object Initialize(BenchmarkInfo benchmark);
    public object WarmupIteration(object oldstate);
    public object PreRun(object oldstate);
    public object Run(object state);
    public object PostRun(object oldstate);
}
