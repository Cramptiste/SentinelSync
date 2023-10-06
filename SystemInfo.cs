using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SentinelSyncV1
{
    public class SystemInfo
    {
        // getting OS informations
        public string GetOsInfos(string param)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            // mo = management object
            foreach (ManagementObject? mo in mos.Get())
            {
                switch (param)
                {
                    case "os":
                        return mo["Caption"]?.ToString() ?? "";
                    // Arch = architecture
                    case "arch":
                        return mo["OSArchitecture"]?.ToString() ?? "";
                    // Osv = version du système d'exploitation
                    case "osv":
                        return mo["CSDVersion"]?.ToString() ?? "";
                }
            }
            return "";
        }
        // getting CPU infos
        public string GetCpuInfos()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(
                @"Hardware\Description\System\CentralProcessor\0",
                RegistryKeyPermissionCheck.ReadSubTree
                );
            if (processor_name != null)
            {
                return processor_name.GetValue("ProcessorNameString").ToString();
            }
            return "";
        }

        // getting GPU infos
        public void GetGpuInfos()
        {
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Videocontroller"))
            {
                foreach (ManagementObject? obj in searcher.Get())
                {
                    Console.WriteLine("Name - " + obj["Name"]);
                    Console.WriteLine("DeviceID - " + obj["DeviceID"]);
                    Console.WriteLine("AdapterRAM - " + obj["AdapterRAM"]);
                    Console.WriteLine("AdapterDACType - " + obj["AdapterDACType"]);
                    Console.WriteLine("Monochrome - " + obj["Monochrome"]);
                    Console.WriteLine("InstalledDisplayDrivers - " + obj["InstalledDisplayDrivers"]);
                    Console.WriteLine("DriverVersion - " + obj["DriverVersion"]);
                    Console.WriteLine("VideoProcessor - " + obj["VideoProcessor"]);
                    Console.WriteLine("VideoArchitecture - " + obj["VideoArchitecture"]);
                    Console.WriteLine("VideoMemoryType - " + obj["VideoMemoryType"]);
                }
            }
        }

    }

}