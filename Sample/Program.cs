using CpuEnergyMeter;

using RAPLNet.Attributes;
using RAPLNet.Benchmark;

namespace Sample
{
    public class VariablesBenchmarks
    {
        [Benchmark("Variables", "Tests local variables")]
        public static long LocalVariable([BenchmarkLoopiterations] long loopIterations)
        {
            Console.WriteLine("inside");
            long localA = 0, localB = 1;
            for (long i = 0; i < loopIterations; i++)
            {
                Console.WriteLine("loop + " + i);
                localA += localB + i;

            }
            return localA;
        }
    }

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

                }.Run(bm, m);

            }
        }
    }
}
