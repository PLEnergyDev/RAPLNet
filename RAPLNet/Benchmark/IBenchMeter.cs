namespace RAPLNet.Benchmark;

interface IBenchMeter
{
    public void Start();
    public object End();
}
interface IBenchMeter<T> : IBenchMeter
{
    object IBenchMeter.End() => End();
    public T End();
}