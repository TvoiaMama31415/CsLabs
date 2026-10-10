public class Program
{
    public static string BuildReport(string[] lines)
    {

        DateTime dataa = DateTime.Now;
        string pobed = "";
        int ochpobed = 0;
        string loot = "";
        int yteshnag = 0;
        int war = 0;
        int osh = 0;
        string chiayteshnag1 = "";
        string chiayteshnag2 = "";
        bool iseventrunning = false;
        const string nach = "Событие началось:";
        foreach (string item in lines)
        {
            (DateTime data, string Info, string Server, string Text) = Razdel(item);
            if (Text.Contains("Событие \"Восстание Ледяного Пламени\" закрыто"))
            {
                iseventrunning = false;
            }
            if (Text.Contains(nach))
            {
                iseventrunning = true;
                dataa = data;
            }
            if (iseventrunning)
            {
                if (Info.Contains("Warning")) war++;
                if (Info.Contains("Error")) osh++;

                if (item.Contains(" объявлены победителями"))
                {
                    int messageStart = item.IndexOf("] ") + 2;
                    int winnerEnd = item.IndexOf(" объявлены победителями");
                    pobed = item.Substring(messageStart, winnerEnd - messageStart);
                }

                if (Server.Contains("Reward") && item.Contains("Ночные совы получили "))
                {
                    const string startText = "Ночные совы получили ";
                    int start = item.IndexOf(startText) + startText.Length;
                    int end = item.IndexOf(" очков", start);
                    ochpobed = int.Parse(item.Substring(start, end - start));
                }

                if (item.Contains("Ночные совы получили ивентовый предмет:"))
                {
                    string startText = "Ночные совы получили ивентовый предмет:";
                    int ind1 = item.IndexOf(startText);
                    loot = item.Substring(ind1 + startText.Length);
                }
                if (item.Contains(" получили утешительную награду: "))
                {
                    int ind1 = item.IndexOf("] ");
                    int ind2 = item.IndexOf(" ", ind1 + 2);
                    chiayteshnag1 = item.Substring(ind1 + 2, ind2 - ind1 - 4);
                    int ind3 = item.IndexOf(" ", ind2 + 1);
                    chiayteshnag2 = item.Substring(ind2 + 1, ind3 - ind2 - 2);
                    string Pol = " получили утешительную награду: ";
                    int ind4 = item.IndexOf(Pol);
                    int ind5 = ind4 + Pol.Length;
                    int ind6 = item.IndexOf(" очков события");
                    yteshnag = int.Parse(item.Substring(ind5, ind6 - ind5));
                }

            }
        }
         string report =
            "# Итоги события: Восстание Ледяного Пламени\n\n" +
            $"Дата: {dataa:dd.MM.yyyy}\n" +
            $"Победитель: {pobed}\n" +
            $"Очки победителя: {ochpobed}\n" +
            $"Ивентовый предмет:{loot}\n" +
            $"Утешительная награда {chiayteshnag1}ых {chiayteshnag2}ов: {yteshnag} очков\n" +
            $"Предупреждений во время события: {war}\n" +
            $"Ошибок во время события: {osh}";
        return report;
    }
    static void Main()
    {
        string[] lines = File.ReadAllLines("event_server.log");
        Console.WriteLine(BuildReport(lines));
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


