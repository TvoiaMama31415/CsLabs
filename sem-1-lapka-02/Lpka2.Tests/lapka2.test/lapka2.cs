using System;
using System.Collections.Generic;
using System.Text;

namespace lapka2.test
{
    public class Program
    {
        public static string CheckConfiguration(int players, int memoryGb, bool isPublic, bool hasPassword)
        {
            if (players <= 0)
                {
                return "Запуск невозможен, некому играть (._.)";
                }
            if (memoryGb<=1)
                {
                return "Запуск невозможен, недостаточно оперативной памяти (;_;)";
                }
            if (players>100)
                {
                return "Че так дофига `Σ(°△°";
                }
            if (isPublic&&!hasPassword)
            {
                return "Анархия нафик (╯°▽°)╯︵ ┻━┻";
            }
            if (!isPublic && hasPassword)
            {
                return "Мы в Чебурнете ＾◡＾";
            }
            if (players > 100 - memoryGb * 10)
            {
                return "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";
            }
            return "Все хорошо, сервер готов к запуску, приятной игры <3";
        }
    }
}
