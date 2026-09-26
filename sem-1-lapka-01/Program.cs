using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the number of online players:");
        int onl = int.Parse(Console.ReadLine());
        Console.WriteLine("rith ^^");
        long Registrtion = 896913322192;
        long Registrtion1 = Registrtion + 1;
        Console.WriteLine("What's the ping?");
        double png = double.Parse(Console.ReadLine());
        int crashCount = 0;
        Console.WriteLine("Did the server crash today?");
        string ans = Console.ReadLine();
        bool wasCrash = ans == "yes";
        if (wasCrash)
        {
            Console.WriteLine("How many times did the server crash?");
            crashCount = int.Parse(Console.ReadLine());
        }
        else
        {
            Console.WriteLine("That's cool ^^");
        }
        Console.WriteLine("|========================================================|");
        Console.WriteLine("|  Here is some brief information about the server:      |");
        Console.WriteLine($"|  Players online: {onl}                                     |");
        Console.WriteLine($"|  In this moment {Registrtion1} people are registered     |");
        if (wasCrash)
        {
            Console.WriteLine($"|  The site went down {crashCount} times today                      |");
        }
        else
        {
            Console.WriteLine("|  The site didn't go down today                         |");
        }
        Console.WriteLine("|========================================================|");
    }
}