using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace sem_1_lab_05_save_manager
{

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class SaveData
    {
        public string Character { get; set; }
        public string Map { get; set; }
        public Position Position { get; set; }
        public int Health { get; set; }
        public List<string> Inventory { get; set; }
    }

    public class SaveEntry
    {
        public string Name { get; set; }
        public string File { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    internal class Program
    {
        const string IndexPath = "index.json";
        const string SavesDir = "saves";
        const string BackupsDir = "backups";

        static List<SaveEntry> LoadIndex(string indexPath)
        {
            if (!File.Exists(indexPath))
            {
                Console.WriteLine($"Файл не найден: {indexPath}");
                Environment.Exit(0);
            }

            try
            {
                string json = File.ReadAllText(indexPath);
                List<SaveEntry> entries = JsonConvert.DeserializeObject<List<SaveEntry>>(json);

                if (entries == null)
                {
                    Console.WriteLine("Индекс пуст или содержит null.");
                    Environment.Exit(0);
                }

                return entries;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка чтения JSON: {ex.Message}");
                Environment.Exit(0);
                return new List<SaveEntry>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка доступа к файлу: {ex.Message}");
                Environment.Exit(0);
                return new List<SaveEntry>();
            }
        }

        static void SaveIndex(string indexPath, List<SaveEntry> entries)
        {
            try
            {
                string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
                File.WriteAllText(indexPath, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка записи индекса: {ex.Message}");
            }
        }

        static SaveData LoadSave(string savesDirectory, SaveEntry entry)
        {
            string path = Path.Combine(savesDirectory, entry.File);

            if (!File.Exists(path))
            {
                Console.WriteLine($"Файл сохранения не найден: {path}");
                return null;
            }

            try
            {
                string json = File.ReadAllText(path);
                SaveData save = JsonConvert.DeserializeObject<SaveData>(json);

                if (save == null)
                {
                    Console.WriteLine("Файл сохранения пуст.");
                    return null;
                }

                return save;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка JSON в {path}: {ex.Message}");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка доступа к {path}: {ex.Message}");
                return null;
            }
        }

        static void SaveGame(string savesDirectory, SaveEntry entry, SaveData save)
        {
            string path = Path.Combine(savesDirectory, entry.File);

            try
            {
                string json = JsonConvert.SerializeObject(save, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка записи сохранения: {ex.Message}");
            }
        }

        static void PrintSave(SaveEntry entry, SaveData save)
        {
            Console.WriteLine($"Имя слота: {entry.Name}");
            Console.WriteLine($"Файл: {entry.File}");
            Console.WriteLine($"Изменено: {entry.UpdatedAt}");
            Console.WriteLine($"Персонаж: {save.Character}");
            Console.WriteLine($"Карта: {save.Map}");
            Console.WriteLine($"Позиция: X={save.Position.X}, Y={save.Position.Y}");
            Console.WriteLine($"Здоровье: {save.Health}");
            Console.WriteLine($"Инвентарь: {string.Join(", ", save.Inventory)}");
        }

        static SaveEntry FindByName(List<SaveEntry> entries, string name)
        {
            foreach (SaveEntry entry in entries)
            {
                if (entry.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return entry;
                }
            }
            return null;
        }

        static void CreateBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            string source = Path.Combine(savesDirectory, entry.File);
            string dest = Path.Combine(backupsDirectory, entry.File);

            if (!File.Exists(source))
            {
                Console.WriteLine($"Файл не найден: {source}");
                return;
            }

            Directory.CreateDirectory(backupsDirectory);
            File.Copy(source, dest, overwrite: true);
            Console.WriteLine($"Резервная копия создана: {dest}");
        }

        static void RestoreBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            string source = Path.Combine(backupsDirectory, entry.File);
            string dest = Path.Combine(savesDirectory, entry.File);

            if (!File.Exists(source))
            {
                Console.WriteLine($"Резервная копия не найдена: {source}");
                return;
            }

            File.Copy(source, dest, overwrite: true);
            Console.WriteLine($"Сохранение восстановлено из: {source}");
        }

        static void DeleteSave(string savesDirectory, string indexPath, List<SaveEntry> entries, SaveEntry entry)
        {
            string path = Path.Combine(savesDirectory, entry.File);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Console.WriteLine($"Файл отсутствует: {path}");
            }

            entries.Remove(entry);
            SaveIndex(indexPath, entries);
            Console.WriteLine($"Сохранение {entry.Name} удалено.");
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Рабочая директория: {Directory.GetCurrentDirectory()}");
            Directory.CreateDirectory(SavesDir);
            Directory.CreateDirectory(BackupsDir);

            List<SaveEntry> entries = LoadIndex(IndexPath);

            while (true)
            {
                Console.WriteLine("\n==== МЕНЮ ====");
                Console.WriteLine("1. Показать список сохранений");
                Console.WriteLine("2. Посмотреть данные сохранения");
                Console.WriteLine("3. Изменить данные персонажа");
                Console.WriteLine("4. Создать резервную копию");
                Console.WriteLine("5. Восстановить резервную копию");
                Console.WriteLine("6. Удалить сохранение");
                Console.WriteLine("7. Выйти");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Сохранений: {entries.Count}");
                        foreach (SaveEntry e in entries)
                            Console.WriteLine($"- {e.Name} ({e.File}), {e.UpdatedAt}");
                        break;

                    case "2":
                        {
                            Console.Write("Введите имя: ");
                            string n = Console.ReadLine();
                            SaveEntry e = FindByName(entries, n);
                            if (e == null) { Console.WriteLine("Не найдено."); break; }
                            SaveData s = LoadSave(SavesDir, e);
                            if (s != null) PrintSave(e, s);
                            break;
                        }

                    case "3":
                        {
                            Console.Write("Введите имя: ");
                            string n = Console.ReadLine();
                            SaveEntry e = FindByName(entries, n);
                            if (e == null) { Console.WriteLine("Не найдено."); break; }
                            SaveData s = LoadSave(SavesDir, e);
                            if (s == null) break;

                            Console.Write($"Персонаж ({s.Character}): ");
                            string c = Console.ReadLine();
                            if (!string.IsNullOrEmpty(c)) s.Character = c;

                            Console.Write($"Карта ({s.Map}): ");
                            string m = Console.ReadLine();
                            if (!string.IsNullOrEmpty(m)) s.Map = m;

                            Console.Write($"X ({s.Position.X}): ");
                            string x = Console.ReadLine();
                            if (int.TryParse(x, out int nx)) s.Position.X = nx;

                            Console.Write($"Y ({s.Position.Y}): ");
                            string y = Console.ReadLine();
                            if (int.TryParse(y, out int ny)) s.Position.Y = ny;

                            Console.Write($"Здоровье ({s.Health}): ");
                            string h = Console.ReadLine();
                            if (int.TryParse(h, out int nh)) s.Health = nh;

                            SaveGame(SavesDir, e, s);
                            e.UpdatedAt = DateTime.Now;
                            SaveIndex(IndexPath, entries);
                            Console.WriteLine("Сохранено.");
                            break;
                        }

                    case "4":
                        {
                            Console.Write("Введите имя: ");
                            string n = Console.ReadLine();
                            SaveEntry e = FindByName(entries, n);
                            if (e == null) { Console.WriteLine("Не найдено."); break; }
                            CreateBackup(SavesDir, BackupsDir, e);
                            break;
                        }

                    case "5":
                        {
                            Console.Write("Введите имя: ");
                            string n = Console.ReadLine();
                            SaveEntry e = FindByName(entries, n);
                            if (e == null) { Console.WriteLine("Не найдено."); break; }
                            RestoreBackup(SavesDir, BackupsDir, e);
                            break;
                        }

                    case "6":
                        {
                            Console.Write("Введите имя: ");
                            string n = Console.ReadLine();
                            SaveEntry e = FindByName(entries, n);
                            if (e == null) { Console.WriteLine("Не найдено."); break; }
                            DeleteSave(SavesDir, IndexPath, entries, e);
                            break;
                        }

                    case "7":
                        Console.WriteLine("Выход...");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
    }
}


//using Newtonsoft.Json;
//public class SaveData
//{
//    public string Character { get; set; }
//    public string Map { get; set; }
//    public Position Position { get; set; }
//    public int Health { get; set; }
//    public List<string> Inventory { get; set; }
//}
//public class Position
//{
//    public int x { get; set; }
//    public int y { get; set; }
//}
//public class SaveEntry
//{
//    public string Name { get; set; }
//    public string File { get; set; }
//    public DateTime UpdatedAt { get; set; }
//}
//public class Program
//{
//    static List<SaveEntry> LoadIndex(string indexPath)
//    {
//        if (!File.Exists(indexPath))
//        {
//            Console.WriteLine($"Файл не найден");
//            Console.WriteLine($"Программа завершена");
//            Environment.Exit(0);
//        }
//        try
//        {
//            string json = File.ReadAllText(indexPath);
//            List<SaveEntry> entries = JsonConvert.DeserializeObject<List<SaveEntry>>(json);
//            if (entries == null)
//            {
//                Console.WriteLine("Индекс пуст.");
//                Environment.Exit(0);
//            }
//            return entries;
//        }
//        catch (JsonException ex)
//        {
//            Console.WriteLine($"Ошибка чтения JSON: {ex.Message}");
//            Environment.Exit(0);
//            return new List<SaveEntry>(); 
//        }
//        catch (IOException ex)
//        {
//            Console.WriteLine($"Ошибка доступа к файлу: {ex.Message}");
//            Environment.Exit(0);
//            return new List<SaveEntry>();
//        }
//    }
//    static SaveData LoadSave(string savesDirectori, SaveEntry entry)
//    {



//    }
//}











//    static List<SaveEntry> LoadIndex(string indexPath)
//    {
//        string json = File.ReadAllText(indexPath);
//        SaveData save = JsonConvert.DeserializeObject<SaveData>(json);
//        return new List<SaveEntry>();

//    }
//    static void Main()
//    {
//        jsonConvert.DeserializeObject<List<SaveEntry>>(json);


//    }
//}