namespace RAPLNet.Benchmark;

public interface IBenchMeter
{
    public void Start(Action a);
    //public object End();
}
//public interface IBenchMeter<T> : IBenchMeter
//{
//    object IBenchMeter.End() => End();
//    public T End();
//}