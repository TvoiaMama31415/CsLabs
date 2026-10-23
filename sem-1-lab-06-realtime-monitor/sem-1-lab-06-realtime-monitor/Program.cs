using System;
using System.ComponentModel.DataAnnotations;

internal class Program
{
    static List<int> listIndexCpu = new List<int>();
    static List<int> listIndexRam = new List<int>();
    static List<int> listIndexTemp = new List<int>();
    private static void Main()
    {
        var monitor = new ServerMonitor();
        monitor.DataReceived += OnDataReceived;
        monitor.Start();

        Console.WriteLine("Мониторинг запущен.");


        Console.ReadLine();

        monitor.Stop();

    }
    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        Console.Clear();

        Console.WriteLine($"{cpu}, {ram}, {temperature}");
        Console.WriteLine("CPU:");
        int[] cpu_promez = IzDoubleVMassiv(listIndexCpu, cpu);
        Console.WriteLine(Grafichek(cpu_promez));
        Console.WriteLine("--------------------------- ");
        Console.WriteLine("   %   10  30  50  70    100");

        Console.WriteLine("RAM:");
        int[] ram_promez = IzDoubleVMassiv(listIndexRam, ram);
        Console.WriteLine(Grafichek(ram_promez));
        Console.WriteLine("--------------------------- ");
        Console.WriteLine("   %   10  30  50  70    100");

        Console.WriteLine("TEMPERATURE:");
        int[] temp_promez = IzDoubleVMassiv(listIndexTemp, temperature);
        Console.WriteLine(Grafichek(temp_promez));
        Console.WriteLine("--------------------------- ");
        Console.WriteLine("   %   10  30  50  70    100");
    }
    static int[] IzDoubleVMassiv(List<int> listIndex, double index)
    {
        if (listIndex.Count < 20)
        {
            listIndex.Add((int)Math.Round(index));
            int[] massivCpu = listIndex.ToArray();
            if (massivCpu.Length < 20)
            {
                int[] slice = new int[20];
                Array.Copy(massivCpu, 0, slice, 0, massivCpu.Length);
                massivCpu = slice;
            }
            return (massivCpu);
        }
        if (listIndex.Count == 20)
        {
            int[] massivCpu = listIndex.ToArray();
            int[] massivCpuResult = new int[20];
            Array.Copy(massivCpu, 1, massivCpuResult, 0, 19);
            massivCpuResult[19] = (int)Math.Round(index);
            listIndex.RemoveAt(0);
            listIndex.Add((int)Math.Round(index));
            return (massivCpuResult);
        }
        else return [6, 7];
    }
    static string Grafichek(int[] mass)
    {
        string res = "";
        for (int i = 0; i < 20; i++)
        {
            int val = mass[i];
            string znach = RenderGraph(val);
            res+=($"{i + 1,4} | {znach} \n");
        }
        return res;
    }
    static string RenderGraph(int value)
    {
        int Chislo = (int)Math.Round(value * 0.2);
        if (Chislo > 20) Chislo = 20;
        string result = "";
        for (int i = 0; i < Chislo; i++)
        {
            result += "#";
        }
        string result1 = new string('.', 20 - result.Length);
        return result + result1;
    }
}




