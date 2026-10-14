using System.Net.Mail;

class Program
{
    static void Main()
    {
        int NomerStroki = 0;
        int SokVStroki = 0;
        int SokVsego = 0;
        int LovyshVsego = 0;
        int ItogNomerStr = 0;
        string[] lines = File.ReadAllLines("../../../файлик.txt");
        foreach (string line in lines)
        {
            string[] razdel = line.Split(" ");
            NomerStroki += 1;
            //foreach (var part in razdel)
            //{
            //    Console.WriteLine(part);
            //}
            int[] nums = new int[razdel.Length];
            int S = 0;
            int L = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int.TryParse(razdel[i], out nums[i]);
                if (nums[i] == -1) L++;
                if (nums[i] == 2) S++;

            }
            SokVsego += S;
            LovyshVsego += L;
            if (S > SokVStroki)
            {
                SokVStroki = S;
                ItogNomerStr = NomerStroki;

            }
        }
        Console.WriteLine($"Всего ловушек:{LovyshVsego}");
        Console.WriteLine($"Всего сокровищ:{SokVsego}");
        Console.WriteLine($"Строка где больше всего сокровищ:{ItogNomerStr}");
    }

}
    
