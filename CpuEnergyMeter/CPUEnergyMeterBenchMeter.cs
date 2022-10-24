using System;
using System.Diagnostics;
using System.Reflection;
using CpuEnergyMeter.Properties;

using RAPLNet.Benchmark;



namespace CpuEnergyMeter
{
    public class EnergyResult : IBenchmarkResult
    {

    }

    public class CPUEnergyMeterBenchMeter : IBenchMeter<EnergyResult>
    {
        Thread mt;
        
        string foo(Barrier b, out Action kill)
        {
            int k;
            Process p = null;
            kill = () =>
            {
                ProcessStartInfo pso = new ProcessStartInfo("/usr/bin/kill");
                pso.ArgumentList.Add("-INT");
                pso.ArgumentList.Add($"{p.Id}");
                pso.RedirectStandardOutput = true;
                using var killprocess = Process.Start(pso);
            };
            ProcessStartInfo psi = new ProcessStartInfo(exec){RedirectStandardOutput=true};
            string s;
            p = new Process() { StartInfo = psi, };

            b.SignalAndWait();
            p.Start();
            b.SignalAndWait();
            s = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return s;

        }
        public CPUEnergyMeterBenchMeter()
        {
        }
        string root => AppDomain.CurrentDomain.BaseDirectory;
        string cpu_energy_meter_root => Path.Combine(root, "cpu-energy-meter");
        string exec => Path.Combine(cpu_energy_meter_root, "cpu-energy-meter");

        public void Prepare()
        {
            var url = "https://github.com/sosy-lab/cpu-energy-meter/releases/download/1.2/cpu-energy-meter-1.2.tar.gz";
            var giturl = "https://github.com/sosy-lab/cpu-energy-meter.git";
            
            var cem_zip = Path.Combine(cpu_energy_meter_root, "cpu-energy-meter-1.2.tar.gz");
            var preparescript = Path.Combine(cpu_energy_meter_root, "getandbuild.sh");
            Directory.CreateDirectory(cpu_energy_meter_root);
            if (!File.Exists(preparescript))
                File.WriteAllText(preparescript, Resources.getandbuild);

            ProcessStartInfo psi = new ProcessStartInfo("/bin/bash");
            psi.ArgumentList.Add(preparescript);
            psi.ArgumentList.Add(cpu_energy_meter_root);
            psi.ArgumentList.Add(giturl);
            //psi.ArgumentList.Add(url);

            var process = Process.Start(psi);
            process.WaitForExit();

            Console.WriteLine(  process.ExitCode);
            
            //string root = string.Empty;
            //Assembly ass = Assembly.GetAssembly(typeof(CpuEnergyMeter.CPUEnergyMeterBenchMeter));
            //if (ass != null)
            //{
            //    root = ass.Location;
            //}
            
        }


        public EnergyResult Start(Action a)
        {
            Barrier b = new Barrier(2);
            string output;
            Action killAction = ()=>Console.WriteLine("unassigned");
            Thread meterThread = new Thread(()=>output = foo(b, out killAction));
            meterThread.Start();
            b.SignalAndWait();
            Thread t = new Thread(() =>
            {
                b.SignalAndWait();
                Thread.Sleep(1000);
                a();
            });
            try
            {
                t.Join();
            }
            finally
            {
                killAction();
                //b.SignalAndWait();
            }
            return new EnergyResult();

        }
    }
}
