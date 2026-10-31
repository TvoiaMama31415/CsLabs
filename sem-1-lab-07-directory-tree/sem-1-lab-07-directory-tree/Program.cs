using System;
using System.IO;
using System.Xml.Linq;

internal class Program
{
    // Current line in file
    private static int _lineIndex;

    // Current nesting level
    private static int _depth;

    // File content
    private static string[] _lines = null!;

    private static void Main()
    
    
    {
        _lines = File.ReadAllLines("tree.txt");

        Console.WriteLine(_lines[0]);

        _lineIndex = 1;

        string treeText = PrintTree();
        Console.Write(treeText);

        Console.ReadLine();
    }

    private static string PrintTree()
    {
        string result = "";
        while (_lineIndex < _lines.Length)
        {
            var line = _lines[_lineIndex].Trim();

            if (line == "")
            {
                _lineIndex += 1;
                continue;
            }

            if (line == "END")
            {
                _depth--;

                _lineIndex++;

                return result ;
            }

            if (line.StartsWith("BEGIN "))
            {
                var name = line.Substring(6);

                result += (GetIndent()+ "├─" + name + "\n");

                _depth++;

                _lineIndex++;

                result+=PrintTree();
            }
        }
        return result;
    }
    private static string GetIndent()
    {
        var resultI = "";

        for (var i = 0; i < _depth; i++)
        {
            resultI += "│ ";
        }

        return resultI;
    }

}
//      1 ошибка:
// var name = line.Substring(5);
// обрезает 5 символов в строке "BEGIN (буква)" и получается " (буква)", а надо просто букву без пробела => меняем 5 на 6
//      2 ошибка:
// в методе PrintTree() if (line == "") _lineIndex увеличивался на 2, а чобы считывать строки друг за другом надо _lineIndex += 1
//      3 ошибка:
// в методе GetIndent() в условиях цикла было i <= _depth, из-за этого появлялись лишние линии, нужно заменить на i < _depth
//      4 ошибка:
// в методе PrintTree() if (line.StartsWith("BEGIN ")) _lineIndex увеличивался после вызова рекурсии заново и в итоге индекс при выполнении if не увеличивается
// 
// Изменила программу, чтобы вывод на экран был только в методе Main()



