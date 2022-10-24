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
            m.Start(()=> Thread.Sleep(1000));

            BenchmarkCollector bc = new BenchmarkCollector();
            var bms = bc.CollectBenchmarks(new Program().GetType().Assembly);
	    Console.WriteLine(bms.Count());
            foreach (var bm in bms)
            {
                new NoGCBenchmarkRunner()
                {

                }.Run(bm);

            }
        }
    }
}
