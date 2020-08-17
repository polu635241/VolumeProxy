using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeProxy
{
    static class Tool
    {
        /// <summary>
        /// 把ReadKey所打印出來的內容消掉
        /// </summary>
        public static void ClearCurrentLine ()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int charCount = Console.CursorLeft;

            Console.SetCursorPosition (0, Console.CursorTop);

            //長出跟當前文字同樣的空格把文字內容洗掉
            for (int i = 0; i <= charCount; i++)
            {
                Console.Write (" ");
            }

            Console.SetCursorPosition (0, Console.CursorTop);
        }
    }
}
