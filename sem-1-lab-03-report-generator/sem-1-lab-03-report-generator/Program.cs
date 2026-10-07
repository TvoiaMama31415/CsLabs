class Program
{
    static void Main()
    {
        DateTime Data = DateTime.Now;
        string Pobed = "";
        int OchPobed = 0;
        string loot = "";
        int YteshNag = 0;
        int war = 0;
        int osh = 0;
        string ChiaYteshNag1 = "";
        string ChiaYteshNag2 = "";
        string[] lines = File.ReadAllLines("event_server.log");
        bool isEventRunning = false;
        const string nach = "Событие началось:";
        foreach (string item in lines)
        {
            (DateTime data, string Info, string Server, string Text) = Razdel(item);
            if (Text.Contains("Событие \"Восстание Ледяного Пламени\" закрыто"))
            {
                isEventRunning = false;
            }
            if (Text.Contains(nach))
            {
                isEventRunning = true;
                Data = data;
            }
            if (isEventRunning)
            {
                if (Info.Contains("Warning")) war++;
                if (Info.Contains("Error")) osh++;

                if (item.Contains(" объявлены победителями"))
                {
                    int messageStart = item.IndexOf("] ") + 2;
                    int winnerEnd = item.IndexOf(" объявлены победителями");
                    Pobed = item.Substring(messageStart, winnerEnd - messageStart);
                }

                if (Server.Contains("Reward") && item.Contains("Ночные совы получили "))
                {
                    const string startText = "Ночные совы получили ";
                    int start = item.IndexOf(startText) + startText.Length;
                    int end = item.IndexOf(" очков", start);
                    OchPobed = int.Parse(item.Substring(start, end - start));
                }

                if (item.Contains("Ночные совы получили ивентовый предмет:"))
                {
                    string startText = "Ночные совы получили ивентовый предмет:";
                    int ind1 = item.IndexOf(startText);
                    loot = item.Substring(ind1 + startText.Length );
                }
                if (item.Contains(" получили утешительную награду: "))
                {
                    int ind1 = item.IndexOf("] ");
                    int ind2 = item.IndexOf(" ", ind1+2);
                    ChiaYteshNag1 = item.Substring(ind1 + 2, ind2 - ind1 - 4);
                    int ind3 = item.IndexOf(" ", ind2+1);
                    ChiaYteshNag2 = item.Substring(ind2+1, ind3 - ind2-2);
                    string Pol = " получили утешительную награду: ";
                    int ind4 = item.IndexOf(Pol);
                    int ind5 = ind4 + Pol.Length;
                    int ind6 = item.IndexOf(" очков события");
                    YteshNag = int.Parse(item.Substring(ind5, ind6 - ind5));
                }

            }
        }
        Console.WriteLine("# Итоги события: Восстание Ледяного Пламени");
        Console.WriteLine($"Дата: {Data.ToString("dd.MM.yyyy")}");
        Console.WriteLine($"Победитель: {Pobed}");
        Console.WriteLine($"Очки победителя: {OchPobed}");
        Console.WriteLine($"Ивентовый предмет:{loot}");
        Console.WriteLine($"Утешительная награда {ChiaYteshNag1}ых {ChiaYteshNag2}ов: {YteshNag} очков");
        Console.WriteLine($"Предупреждений во время события: {war}");
        Console.WriteLine($"Ошибок во время события: {osh}");
    }
    public static (DateTime, string, string, string) Razdel(string item)
    {
        int ind1 = item.IndexOf(" ");
        int ind2 = item.IndexOf(" ", ind1 + 1);
        string datestr = item.Substring(0, ind2);
        DateTime data = DateTime.Parse(datestr);
        int ind3 = item.IndexOf("[");
        int ind4 = item.IndexOf("]");
        int ind5 = item.IndexOf("[", ind3 + 1);
        int ind6 = item.IndexOf("]", ind4 + 1);
        string Info = item.Substring(ind3, ind4 - ind3);
        string Server = item.Substring(ind5, ind6 - ind5);
        string Text = item.Substring(ind6 + 1);
        return (data, Info, Server, Text);
    }
}

