using System;
using System.Diagnostics;
using System.Management;
using SentinelSyncV1;

namespace SentinelSyncV1
{
    class Program
    {
        static string RefreshCpuInfos()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            dynamic firstVal = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(100);
            dynamic val = cpuCounter.NextValue();

            decimal roundVal = Convert.ToDecimal(val);
            roundVal = Math.Round(roundVal, 2);

            return roundVal + " %";
        }

        static void RefreshRamInfos()
        {
            Console.WriteLine("available : " + SentinelSyncV1.SystemInfo.FormatSize(SentinelSyncV1.SystemInfo.GetAvailPhys()));
            Console.WriteLine("used : " + SentinelSyncV1.SystemInfo.FormatSize(SentinelSyncV1.SystemInfo.GetUsedPhys()));
            Console.WriteLine("total : " + SentinelSyncV1.SystemInfo.FormatSize(SentinelSyncV1.SystemInfo.GetTotalPhys()));
        }
        /*
        static string getRamCounter()
        {
            PerformanceCounter ramCounter = new PerformanceCounter();
            ramCounter.CategoryName = "Memory";
            ramCounter.CounterName = "Available MBytes";

            dynamic firstValue = ramCounter.NextValue();
            System.Threading.Thread.Sleep(100);
            dynamic val = ramCounter.NextValue();

            decimal roundVar = Convert.ToDecimal(val);
            roundVar = Math.Round(roundVar, 2);

            return roundVar.ToString();
        }
        */

        static void GetAllSystemInfos()
        {
            var si = new SystemInfo();
            Console.WriteLine(si.GetCpuInfos());
            Console.WriteLine(si.GetOsInfos("os"));
            Console.WriteLine(si.GetOsInfos("arch"));
            Console.WriteLine(si.GetOsInfos("osv"));
            si.GetGpuInfos();
        }
        static void ShowCpuUse()
        {
            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    return;
                }
                Console.WriteLine("(ENTER to stop) processor utilisation : " + RefreshCpuInfos());
            }
        }

        static void ShowRamUse()
        {
            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    return;
                }
                Console.WriteLine("(ENTER to stop) RAM utilisation : ");
                RefreshRamInfos();
                System.Threading.Thread.Sleep(500);
            }
        }

        static void Main(string[] args)
        {
            GetAllSystemInfos();
            int choice = 0;

            Console.WriteLine("1 - show processor utilisation");
            Console.WriteLine("2 - show RAM");
            Console.WriteLine("3 - show PC temperature");
            Console.WriteLine("4 - show stockage infos");
            Console.WriteLine("5 - connexion infos");

            choice = outils.AskNumberBetween("enter a choice : ", 1, 5);
            switch (choice)
            {
                case 1:
                    ShowCpuUse();
                    break;
                case 2:
                    ShowRamUse();
                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;

            }
        }
    }
}
