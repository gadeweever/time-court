using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace hbreaktest
{
    public partial class DateTimeOverlay : UserControl
    {
        List<DayofWeek> days;
        public DateTimeOverlay()
        {
            InitializeComponent();
            this.LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
            this.LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;
            days = new List<DayofWeek>();
            createList();
            
        }

        private void createList()
        {
            string[] dayNames = {"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"};

            for (int i = 0; i < dayNames.Length; i++ )
            {
                days.Add(new DayofWeek());
                days[i].setNum(dayNames[i]);
            }
            circuitDayPicker.ItemsSource = null;
            circuitDayPicker.ItemsSource = days;
        }
    }
}
