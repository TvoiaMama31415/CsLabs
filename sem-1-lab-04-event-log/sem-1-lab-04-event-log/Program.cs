using System;
using System.IO;
using System.Collections.Generic;
public class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("event_server.log");
        LogEntry[] all = ParseLog(lines);
        Console.WriteLine("Всего записей: " + all.Length);
        LogEntry[] errors = FilterByLevel(all, "Error");
        Console.WriteLine("Ошибок: " + errors.Length);
        LogEntry[] serverEntries = FilterByCategory(all, "Server");
        Console.WriteLine("Записей категории Server: " + serverEntries.Length);
        LogEntry[] found = Search(all, "connection");
        Console.WriteLine("Найдено по тексту: " + found.Length);
        LogEntry[] byDate = FilterByDate(all, all[0].Timestamp);
        Console.WriteLine("Записей за дату: " + byDate.Length);
        Console.WriteLine("Fatal: " + CountByLevel(all, "Fatal"));
        Console.WriteLine("Статус сервера: " + GetServerStatus(all));
    }
    public static LogEntry[] ParseLog(string[] lines)
    {
        LogEntry[] entries = new LogEntry[lines.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            LogEntry entry = new LogEntry();
            string line = lines[i];
            entry.Timestamp = DateTime.Parse(line.Substring(0, 23));

            int firstOpenBracket = line.IndexOf('[');
            int firstCloseBracket = line.IndexOf(']', firstOpenBracket);
            int secondOpenBracket = line.IndexOf('[', firstCloseBracket);
            int secondCloseBracket = line.IndexOf(']', secondOpenBracket);

            entry.Level = line.Substring(firstOpenBracket + 1, firstCloseBracket - firstOpenBracket - 1);
            entry.Category = line.Substring(secondOpenBracket + 1, secondCloseBracket - secondOpenBracket - 1);
            entry.Message = line.Substring(secondCloseBracket + 2);
            entries[i] = entry;
        }
        return entries;
    }
    public static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
    {
        var result = new List<LogEntry>();
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == level)
            {
                result.Add(entries[i]);
            }
        }
        return result.ToArray();
    }
    public static LogEntry[] FilterByCategory(LogEntry[] entries, string Category)
    {
        var result = new List<LogEntry>();
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Category == Category)
            {
                result.Add(entries[i]);
            }
        }
        return result.ToArray();
    }
    public static LogEntry[] Search(LogEntry[] entries, string text)
        {
            var result = new List<LogEntry>();
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Message.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(entries[i]);
                }
            }
            return result.ToArray();
        }
    public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
    {
        var result = new List<LogEntry>();
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Timestamp.Date == date.Date)
            {
                result.Add(entries[i]);
            }
        }
        return result.ToArray();
    }
    public static int CountByLevel(LogEntry[] entries, string level)
        {
            int count = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    count++;
                }
            }
            return count;
        }

    public static string GetServerStatus(LogEntry[] entries)
    {
        bool hasError = false;
        bool hasFatal = false;
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == "Error")
            {
                hasError = true;
            }
            if (entries[i].Level == "Fatal" &&
                entries[i].Category == "Server")
            {
                hasFatal = true;
            }
        }

        if (hasFatal)
            return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";

        if (hasError)
            return "Есть ошибки: требуется проверка";

        return "Сервер работает штатно";
    }
}

