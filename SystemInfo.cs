using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SentinelSyncV1
{
    public class SystemInfo
    {
        public static object System { get; internal set; }

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


        #region specificals functions about RAM
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx(ref MEMORY_INFO mi);


        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength; 
            public uint dwMemoryLoad;
            public ulong ullTotalPhys; 
            public ulong ullAvailPhys; 
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual; 
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        public static string FormatSize(double size)
        {
            double d = (double)size;
            int i = 0;
            while ((d > 1024) && (i < 5))
            {
                d /= 1024;
                i++;
            }
            string[] unit = { "B", "KB", "MB", "GB", "TB" };
            return (string.Format("{0} {1}", Math.Round(d, 2), unit[i]));
        }

        public static MEMORY_INFO GetMemoryStatus()
        {
            MEMORY_INFO mi = new MEMORY_INFO();
            mi.dwLength = (uint)Marshal.SizeOf(mi);
            GlobalMemoryStatusEx(ref mi);
            return mi;
        }

 
        public static ulong GetAvailPhys()
        {
            MEMORY_INFO mi = GetMemoryStatus();
            return mi.ullAvailPhys;
        }

        // get used memory
        public static ulong GetUsedPhys()
        {
            MEMORY_INFO mi = GetMemoryStatus();
            return (mi.ullTotalPhys - mi.ullAvailPhys);
        }

        // get total physical memory
        public static ulong GetTotalPhys()
        {
            MEMORY_INFO mi = GetMemoryStatus();
            return mi.ullTotalPhys;
        }
        #endregion

        
    }
    

}
