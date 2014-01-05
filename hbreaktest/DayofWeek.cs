using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hbreaktest
{
    //find a better way to do this entire thing. it's sickening
    public class DayofWeek
    {
        public String name { get; set; }
        public int daynum { get; set; }
        
        public DayofWeek()
        {
            name = "";
            daynum = -1;
        }

        public void setNum(string day)
        {
            this.name = day;

            if (day.CompareTo("Monday") == 0)
                this.daynum = 1;
            else if (day.CompareTo("Tuesday") == 0)
                this.daynum = 2;
            else if (day.CompareTo("Wednesday") == 0)
                this.daynum = 3;
            else if (day.CompareTo("Thursday") == 0)
                this.daynum = 4;
            else if (day.CompareTo("Friday") == 0)
                this.daynum = 5;
            else if (day.CompareTo("Saturday") == 0)
                this.daynum = 6;
            else
                this.daynum = 0;
        }

        public List<DayofWeek> sortDays(List<DayofWeek> list)
        {
            List<DayofWeek> newlist = new List<DayofWeek>();
            int count = list.Count;
            DayofWeek least = list[0];
            while (list.Count > 0)
            {

                for (int i = 0; i < count - 1; i++)
                {
                    if (least.daynum > list[i + 1].daynum)
                        least = list[i];
                }
                newlist.Add(least);
                list.Remove(least);
                count = list.Count;
            }

            return newlist;
        }


            

    }
}
