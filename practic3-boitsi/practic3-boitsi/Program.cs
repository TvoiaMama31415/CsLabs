class Program
{
    static void Main()
    {
        int MaxOch = 0;
        int BoethMaxOch = 0;
        int MaxSrRez = 0;
        int BoethMaxSrRez = 0;
        int MaxChislo = 0;
        int[][] battles =
        {
            new[] {15,20,10},
            new[] {30,25},
            new[] {40},
            new[] {20,20,15,10}
        };
        for(int i=0;i<battles.Length;i++)
        {
            int Summi = battles[i].Sum();
            int SrRez = battles[i].Sum()/battles[i].Count();
            for (int j = 0; j < battles[i].Length; j++)
            {
                if (battles[i][j] > MaxChislo)

                {
                    MaxChislo = battles[i][j];
                }
            }
            if (SrRez > MaxSrRez)
            {
                MaxSrRez = SrRez;
                BoethMaxSrRez = i;
            }
            if (Summi > MaxOch)
            {
                MaxOch = Summi;
                BoethMaxOch = i;
            }
        }
        Console.WriteLine($"{BoethMaxOch} боец - боец с max очками");
        Console.WriteLine($"Максимальный средний результат у {BoethMaxSrRez} бойца");
        Console.WriteLine($"Самый результативный бой равен {MaxChislo}");
    }
}
    
        