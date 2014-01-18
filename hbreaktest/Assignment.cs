using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hbreaktest
{
    public class Assignment
    {
        public String name {get; set;}
        public List<AssignmentTask> tasks { get; set; }
        public bool isCircuitScheduled { get; set; }
        public List<DayofWeek> days { get; set; }
        public String firstTask { get; set; }
        public bool isScheduled { get; set; }
        public List<DateTime> times { get; set; }


        public Assignment()
        {
            name = "";
            tasks = new List<AssignmentTask>();
            isCircuitScheduled = false;
            days = new List<DayofWeek>();
            firstTask = "";
            isScheduled = false;
            times = new List<DateTime>();
        }

        public Assignment(String a)
        {
             name = a;
             tasks = new List<AssignmentTask>();
             isCircuitScheduled = false;
             days = new List<DayofWeek>();
             firstTask = "";
             isScheduled = false;
             times = new List<DateTime>();
        }

        public String getName()
        {
            return this.name;
        }

        public void addTask(AssignmentTask a)
        {
            this.tasks.Add(a);
        }

        public List<AssignmentTask> getTasks()
        {
            return this.tasks;
        }

        public void setDays(List<DayofWeek> a)
        {
            this.days = a;
        }

        public void setFirstTask()
        {
            if (tasks.Count > 0)
                this.firstTask = tasks[0].name;
            else
                this.firstTask = "";
        }


    }
}
