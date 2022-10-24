namespace RAPLNet.Benchmark;


public interface IBenchmarkResult
{

}

public interface IBenchMeter
{
    public IBenchmarkResult Start(Action a);
}
public interface IBenchMeter<TResult> : IBenchMeter where TResult : IBenchmarkResult
{
    IBenchmarkResult IBenchMeter.Start(Action a) => Start(a);
    new TResult Start(Action a);
    //public object End();
}
//public interface IBenchMeter<T> : IBenchMeter
//{
//    object IBenchMeter.End() => End();
//    public T End();
//}