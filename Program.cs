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
        
        static void Main(string[] args)
        {
            GetAllSystemInfos();
            int choice = 0;

            Console.WriteLine("1 - show processor utilisation");
            Console.WriteLine("2 - show RAM utilisation");
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
