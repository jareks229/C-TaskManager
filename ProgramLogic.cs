using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Warsztaty_1
{
    public class ProgramLogic
    {
        private List<TaskModel> TaskModelList = new List<TaskModel>();

        private const string _path = @"data.csv";

        public int TaskCount
        {
            get
            {
                return TaskModelList.Count;
            }
        }

        public void AddTask(string description, DateTime from, DateTime? to, bool isImportant)
        {
            var task = new TaskModel(description, from, to, isImportant);

            TaskModelList.Add(task);
        }

        public void RemoveTask(int taskNumber)
        {
            int index = taskNumber - 1;

            //wtedy nie mieliśmy lambdy ale dodałem gdy wszedł moduł zaawansowany C#
            TaskModelList.Sort((x, y) => { return x.StartDate.CompareTo(y.StartDate); });

            TaskModelList.RemoveAt(index);
        }

        public void ShowTasks()
        {
            Console.WriteLine("------------------------------ LISTA ZADAN -------------------------------");

            TaskModelList.Sort((x, y) => { return x.StartDate.CompareTo(y.StartDate); });

            if (TaskModelList.Count == 0)
            {
                Console.WriteLine("Brak zadan do wykonania!");
                return;
            }

            Console.WriteLine("| {0} | {1} | {2} | {3} | {4} |",
                    "#".PadLeft(4),
                    "Nazwa".PadRight(30),
                    "Data od".PadRight(10),
                    "Data do".PadRight(10),
                    "Priorytet"
                    );

            for (var i = 0; i < TaskModelList.Count; i++)
            {
                var tm = TaskModelList[i];

                var desc = tm.Description;
                if (desc.Length > 28) desc = $"{desc.Substring(0, 25)}...";

                Console.WriteLine("| {0} | {1} | {2} | {3} | {4} |",
                    (i + 1).ToString().PadLeft(4),
                    desc.PadRight(30),
                    tm.StartDate.ToString("yyyy-MM-dd"),
                    tm.EndDate.HasValue ? tm.EndDate.Value.ToString("yyyy-MM-dd") : "N/A".PadRight(10),
                    tm.IsImportant ? "(!)".PadRight(9) : "".PadRight(9)
                    );
            }

        }

        public void SaveTasks()
        {
            TaskModelList.Sort((x, y) => { return x.StartDate.CompareTo(y.StartDate); });

            if (TaskModelList.Count == 0)
            {
                Console.WriteLine("Brak zadan do zapisania!");
                return;
            }

            using (var writer = new StreamWriter(Path.GetFullPath(_path), false))
            {
                foreach (var task in TaskModelList)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4}",
                        task.Description,
                        task.StartDate.ToString("yyyy-MM-dd"),
                        task.EndDate.HasValue ? task.EndDate.Value.ToString("yyyy-MM-dd") : "",
                        task.IsImportant ? "T" : "N",
                        task.AllDay ? "T" : "N"
                    );
                }
            }

            Console.WriteLine("Zapisano do pliku!");
        }

        public void LoadTasks()
        {
            if (File.Exists(_path))
            {
                Console.WriteLine("(!) Nadpisujesz obecne zadania !");
            }

            int recordLoaded = 0;

            using (var reader = new StreamReader(_path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var record = line.Split(',');

                    if (record.Length != 5)
                    {
                        Console.WriteLine("(!) Zły plik ! Spróbuj inny !");
                        TaskModelList.Clear();
                        break;
                    }

                    bool fromParsed = DateTime.TryParse(record[1], out var from);
                    if (fromParsed == false)
                    {
                        Console.WriteLine($"(!) Błąd daty ! (Data od) !");
                        Console.WriteLine($"POMIJAM: {record}");
                        continue;
                    }

                    DateTime? to = null;
                    if (record[2] != string.Empty)
                    {
                        bool toParsed = DateTime.TryParse(record[2], out var toNullable);
                        if (fromParsed == false)
                        {
                            Console.WriteLine("(!) Blad struktury rekordu (Data od) !");
                            Console.WriteLine( "POMIJAM: {0}", record);
                            continue;
                        }

                        to = toNullable;
                    }

                    bool isImportant = record[3] == "T" ? true : false;
                    bool isAllDay = record[4] == "T" ? true : false;

                    var task = new TaskModel(record[0], from, to, isImportant);

                    TaskModelList.Add(task);
                    recordLoaded++;
                }
            }

            Console.WriteLine($"Wczytano {recordLoaded} zadań");
            Console.WriteLine($"W pamieci jest {TaskCount} zadań");
        }
    }
}
