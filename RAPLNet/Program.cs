using RAPLNet.Benchmark;

namespace RAPLNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkCollector bc = new BenchmarkCollector();
            var bms = bc.CollectBenchmarks(new Program().GetType().Assembly);
            foreach(var bm in bms)
            {
                new NoGCBenchmarkRunner()
                {

                }.Run(bm);

            }
        }
    }
}