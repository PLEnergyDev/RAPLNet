using CpuEnergyMeter;

using RAPLNet.Benchmark;

namespace Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m = new CPUEnergyMeterBenchMeter();
            m.Prepare();

            BenchmarkCollector bc = new BenchmarkCollector();
            var bms = bc.CollectBenchmarks(new Program().GetType().Assembly);
            foreach (var bm in bms)
            {
                new NoGCBenchmarkRunner()
                {

                }.Run(bm);

            }
        }
    }
}