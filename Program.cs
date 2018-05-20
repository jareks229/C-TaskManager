using System;
using System.Data;

namespace Warsztaty_1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var listManager = new ProgramLogic();
            Console.WriteLine("******WITAJ W PROGRAMIE TYPU TASK MANAGER !********");
            Console.WriteLine("***************************************************");
            Console.WriteLine("Dostępne polecenia: ");
            string[] avalibleCommands = new[]
            {
                "add -> Dodaj zadanie", "remove -> Usuń zadanie", "show -> Pokaż aktualne zadania", "save -> Zapisz",
                "load -> Wczytaj (z pliku)",
                "aval -> Dostępne polecenia"
            };
            foreach (var aval in avalibleCommands)
            {
                Console.WriteLine(aval);
            }

            do
            {
                string command;

                Console.WriteLine("Wpisz polecenie: ");
                command = Console.ReadLine();

                if (command == "add")
                {

                    Console.WriteLine("--- DODAWANIE ZADANIA ---");
                    Console.WriteLine("*************************");

                    var desc = AskForString("Podaj nazwe zadania");

                    var isImportant = AskForBool("Czy zadanie ma byc oznaczone jako wazne? (T/N): ");
                    var isAllDayTask = AskForBool("Czy zadanie ma byc calodniowe? (T/N): ");

                    DateTime from;
                    DateTime? to = null;

                    if (isAllDayTask)
                    {
                        from = AskForDate("Data zadania");
                    }
                    else
                    {
                        from = AskForDate("Data rozpoczecia zadania");
                        to = AskForDate("Data zakonczenia zadania");
                    }

                    listManager.AddTask(desc, from, to, isImportant);
                    
                }

                if (command == "remove")
                    {
                        Console.WriteLine("--- USUWANIE ZADANIA ---");
                        Console.WriteLine("************************");

                        if (listManager.TaskCount == 0)
                        {
                            Console.WriteLine("Brak zadan!");
                            break;
                        }

                        int numberOfTask = AskForNumberOfTask("Podaj nr zadania", listManager.TaskCount);

                        listManager.RemoveTask(numberOfTask);
                        

                    }

                if (command == "show")
                {
                    listManager.ShowTasks();
                    
                }

                if (command == "save")
                {
                    listManager.SaveTasks();
                    
                }

                if (command == "load")
                {
                    listManager.LoadTasks();
                    

                }

                if (command == "aval")
                {
                    foreach (var avalible in avalibleCommands)
                    {
                        Console.WriteLine(avalible);
                    }

                    
                }

            } while (true);

        }

        private static DateTime AskForDate(string dateName)
        {
            Console.Write("{0} (rrrr-mm-dd): ", dateName);
            var dateString = Console.ReadLine();
            var dateStringParseOk = DateTime.TryParse(dateString, out var date);
            if (!dateStringParseOk)
            {
                Console.WriteLine($"Podana data {dateString} jest nieprawidlowa. Sprobuj jeszcze raz stosujac format rrrr-mm-dd!");
                AskForDate(dateName);
            }

            return date;
        }

        //Podpatrzone u Rafała jak robił:
        private static bool AskForBool(string question)
        {
            Console.Write($"{question} (T/N):");
            var answer = Console.ReadLine().ToLower();

            if (answer != "t" && answer != "n")
            {
                Console.WriteLine("Odpowiadaj tylko 'T' lub 'N'!");
                AskForBool(question);
            }

            return answer == "t";
        }

        private static string AskForString(string question)
        {
            Console.Write($"{question}: ");
            var result = Console.ReadLine();

            return result;
        }

        private static int AskForNumberOfTask(string question, int count)
        {
            var input = AskForString(question);
            var parsedSuccesfully = int.TryParse(input, out int i);
            if (!parsedSuccesfully)
            {
                Console.WriteLine("Nie podałeś liczby !");
                i = AskForNumberOfTask(question, count);
            }

            if (i < 0 || i > count)
            {
                Console.WriteLine($"Podałeś {count}, a lista zawiera elementy 1-{1}");
                i = AskForNumberOfTask(question, count);
            }

            return i;
        }
    }
    }

    public class TaskModel
    {
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsImportant { get; set; }

        public bool AllDay { get; set; }

        public TaskModel(string description, DateTime from, DateTime? to, bool isImportant)
        {
            Description = description;
            StartDate = from;
            EndDate = to;
            IsImportant = isImportant;

            if (to.HasValue)
            {
                AllDay = true;
            }
        }
    }

