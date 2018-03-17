using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
     public static class ConsoleEx
    {
        public static void WriteLine(ConsoleColor color, string line, string[] args = null)
        {
            Console.ForegroundColor = color;
            if (args != null) Console.WriteLine(line, args);
            else Console.WriteLine(line);
        }

        public static void Write(ConsoleColor color, string line, string[] args = null)
        {
            Console.ForegroundColor = color;
            if (args != null) Console.Write(line, args);
            else Console.Write(line);
        }
    }
}
