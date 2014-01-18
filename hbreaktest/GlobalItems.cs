using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hbreaktest
{
    public static class GlobalItems
    {
        
        public static List<Assignment> _appCircuits;
        public static int _indexVal;
        public static AssignmentTask _taskVal;
        public static Assignment _assignmentVal;

        #region DeclarationDefinitions
        //global circuit list used for app
        public static List<Assignment> AppCircuits
        {
            get
            {
                return _appCircuits;
            }
            set
            {
                _appCircuits = value;
            }

        }

        //used to identify the current working circuit INDEX. set on NavigationFrom MainPage
        public static int CurrentCircuitIndex
        {
            get
            {
                return _indexVal;
            }
            set
            {
                _indexVal = value;
            }
        }

        //used to identify the current working AssignmentTask OBJECT. set on NavigationFrom CircuitBuilder
        public static AssignmentTask CurrentTask
        {
            get
            {
                return _taskVal;
            }
            set
            {
                _taskVal = value;
            }
        }

        //used to identify the current working circuit OBJECT. set on NavigationFrom MainPage
        public static Assignment CurrentCircuit
        {
            get
            {
                return _assignmentVal;
            }
            set
            {
                _assignmentVal = value;
            }
        }
        #endregion

        #region DeclarationMethods
        //returns the index of an assignment when searched by its name, -1 if it does not exists
        public static int GetCircuitIndexByName(string a)
        {
            foreach(Assignment assignment in _appCircuits)
            {
                if (assignment.getName().CompareTo(a) == 0)
                    return _appCircuits.IndexOf(assignment);
            }

            return -1;
        }

        //returns the index of an assignmentTask when searched by its name, -1 if it does not exists
        public static int GetTaskIndexByName(string a)
        {
            foreach (AssignmentTask assignment in _assignmentVal.tasks)
            {
                if (assignment.getName().CompareTo(a) == 0)
                    return _assignmentVal.tasks.IndexOf(assignment);
            }

            return -1;
        }

        public static void TaskAdd(AssignmentTask task)
        {
            if (!(_assignmentVal.tasks.Contains(_taskVal)))
                _assignmentVal.tasks.Add(_taskVal);
            else
            {
                int index = GetTaskIndexByName(_taskVal.name);
                _assignmentVal.tasks[index].name = task.name;
                _assignmentVal.tasks[index].reps = task.reps;
                _assignmentVal.tasks[index].hours = task.hours;
                _assignmentVal.tasks[index].minutes = task.minutes;
                _assignmentVal.tasks[index].seconds = task.seconds;
                _assignmentVal.tasks[index].timeText = task.timeText;
                
            }
            

        }
        #endregion

        public static void SaveStorageData()
        {
            using (var filesystem = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fs = new IsolatedStorageFileStream(
                  "assignments.dat", FileMode.Create, filesystem))
                {
                    var serializer = new System.Runtime.Serialization
                      .Json.DataContractJsonSerializer(typeof(List<Assignment>));
                    serializer.WriteObject(fs, _appCircuits);
                }
            }
        }
        
        //Sorting algorithm for list of days. 
        //Creates a new list, sets the first element as the least, and iterates to find the LEAST element
        // adds element to new list, and then removes the object from the old list.
        // repeat until old list is empty. New list will be sorted
        public static List<DayofWeek> SortDays(List<DayofWeek> list)
        {
            List<DayofWeek> newlist = new List<DayofWeek>();
            int count = list.Count;
            DayofWeek least;
            while (list.Count > 0)
            {
                least = list[0];
                for (int i = 0; i < count - 1; i++)
                {
                    if (least.daynum > list[i + 1].daynum)
                        least = list[i + 1];
                }
                newlist.Add(least);
                list.Remove(least);
                count = list.Count;     
            }
            return newlist;
        }

        public static void AddCircuitToSchedule(DateTime time)
        {
            DateTime first = time;
            Reminder begin = new Reminder(_assignmentVal.name);



            foreach (AssignmentTask task in GlobalItems.CurrentCircuit.tasks)
            {
                first = first.AddHours(task.hours);
                first = first.AddMinutes(task.minutes);
                first = first.AddSeconds(task.seconds);

                // wow, such reminder
                Reminder wow = new Reminder(task.name);
                wow.RecurrenceType = RecurrenceInterval.None;
                wow.Content = "Next Task: " + task.name;
                wow.BeginTime = first;
                wow.ExpirationTime = first;
                wow.NavigationUri = new Uri("/CircuitBuilder.xaml?index=" + GlobalItems.CurrentCircuitIndex, UriKind.Relative);
                ScheduledActionService.Add(wow);
                _assignmentVal.times.Add(first);
            }
        }

        public static void RemoveCircuitFromSchedule()
        {
            foreach (AssignmentTask task in _assignmentVal.tasks)
            {
                try
                {
                    ScheduledActionService.Remove(task.name);
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
            }

            _assignmentVal.times.Clear();
        }
    }
}
