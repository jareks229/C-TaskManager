using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class TaskModel
    {
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsImportant { get; set; }
        public bool IsAllDayTask { get; set; }
        public SettingsEnum Settings { get; set; }

        public TaskModel(string description, DateTime startTime)
        {
            Description = description;
            StartTime = startTime;
        }
    }
}
