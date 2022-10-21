using System;
using System.Diagnostics;
using System.Reflection;
using CpuEnergyMeter.Properties;

using RAPLNet.Benchmark;


namespace CpuEnergyMeter
{
    public class CPUEnergyMeterBenchMeter : IBenchMeter
    {
        public void Prepare()
        {
            var url = "https://github.com/sosy-lab/cpu-energy-meter/releases/download/1.2/cpu-energy-meter-1.2.tar.gz";
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var cpu_energy_meter_root = Path.Combine(root, "cpu_energy_meter");
            var cem_zip = Path.Combine(cpu_energy_meter_root, "cpu-energy-meter-1.2.tar.gz");
            var preparescript = Path.Combine(cpu_energy_meter_root, "getandbuild.sh");
            Directory.CreateDirectory(cpu_energy_meter_root);
            if (!File.Exists(preparescript))
                File.WriteAllText(preparescript, Resources.getandbuild);

            ProcessStartInfo psi = new ProcessStartInfo("/bin/sh");
            psi.ArgumentList.Add(preparescript);

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
        public object End()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}