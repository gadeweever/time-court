using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hbreaktest
{

    public class AssignmentTask
    {
        public string name { get; set; }
        public int reps { get; set; }

        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }

        public string timeText { get; set; }

        public AssignmentTask()
        {
            name = "";
            reps = 0;
            hours = 0;
            minutes = 0;
            seconds = 0;
            timeText = "";
        }

        public AssignmentTask(string a, int b, int c, int d, int e, string f)
        {
            name = a;
            reps = b;
            hours = c;
            minutes = d;
            seconds = e;
            timeText = f;
        }

        public string getName()
        {
            return this.name;
        }

        public int getReps()
        {
            return this.reps;
        }

        #region TimeGetters
        public int getHours()
        {
            return this.hours;
        }
        public int getMinutes()
        {
            return this.minutes;
        }
        public int getSeconds()
        {
            return this.seconds;
        }
        #endregion

    }
}
