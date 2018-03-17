using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w programie do układania zadań !");
            
            string command = "      ";
            do
            {
                ConsoleEx.Write(ConsoleColor.Blue, "Wpisz komendę: ");
                command = Console.ReadLine();
                
                
            } while (command != "exit");
        }
    }
}
