using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RAPLNet.Attributes;

namespace Benchmarks.Sample;

public class VariablesBenchmarks
{
    [Benchmark("Variables", "Tests local variables")]
    public static long LocalVariable([BenchmarkLoopiterations] long loopIterations)
    {
        Console.WriteLine("inside");
        long localA = 0, localB = 1;
        for (long i = 0; i < loopIterations; i++)
        {
            Console.WriteLine("loop + "+i);
            localA += localB + i;
            
        }
        return localA;
    }
}
