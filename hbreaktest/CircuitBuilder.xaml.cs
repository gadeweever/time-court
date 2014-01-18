using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using Windows.UI.ViewManagement;
using Microsoft.Phone.Scheduler;

namespace hbreaktest
{
    public partial class CircuitBuilder : PhoneApplicationPage
    {
        
        ApplicationBarIconButton appMenuPlay;
        ApplicationBarIconButton appMenuAdd;
        //control variable for application buttons
        private Popup popup;
        DateTimeOverlay ovr;
        public bool isPicking;
        public bool isPlaying;
        int taskIndex;

        Assignment circuit;
      
        public CircuitBuilder()
        {
            InitializeComponent();
            
            appMenuPlay = (ApplicationBarIconButton)this.ApplicationBar.Buttons[2];
            appMenuAdd = (ApplicationBarIconButton)this.ApplicationBar.Buttons[0];
            circuit = new Assignment();
            ovr = new DateTimeOverlay();
            this.popup = new Popup();
            isPicking = false;
            isPlaying = false;
            taskIndex = -2;
        }

        #region NavigationOverrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            circuitTitle.Text = GlobalItems.CurrentCircuit.getName();
            taskList.ItemsSource = null;
            taskList.ItemsSource = GlobalItems.CurrentCircuit.tasks;

            //handle visiblility
            if (GlobalItems.CurrentCircuit.isCircuitScheduled)
            {
                System.Diagnostics.Debug.WriteLine(ContentPanel.ActualHeight + " " + scheduledPanel.ActualHeight + " " + taskList.MaxHeight);
                appMenuPlay.IsEnabled = false;
                scheduledPanel.Visibility = System.Windows.Visibility.Visible;
                taskList.Height = Application.Current.Host.Content.ActualHeight * 0.4275;
            }
            else
                taskList.Height = Application.Current.Host.Content.ActualHeight * 0.6625;

            
            int j = 0;
            //System.Diagnostics.Debug.WriteLine(GlobalItems.CurrentCircuit.name + " " + GlobalItems.CurrentCircuit.days.Count);
            if (j == GlobalItems.CurrentCircuit.days.Count)
                circuitDayBox.Content = "once";

            CheckTimeForUI();
            

            
        }

        private void CheckTimeForUI()
        {
            if (GlobalItems.CurrentCircuit.isScheduled)
            {
                int length = GlobalItems.CurrentCircuit.tasks.Count;
                DateTime now = DateTime.Now;
                for (int i = 0; i < length; i++)
                {
                    if (GlobalItems.CurrentCircuit.times[i].CompareTo(now) < 0)
                    {
                        currentTaskName.Text = GlobalItems.CurrentCircuit.tasks[i - 1].name;
                        return;
                    }
                }
            }
            else
                currentTaskName.Text = "none";
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (isPicking)
            {
                e.Cancel = true;
                Return();
            }
        }
        #endregion

        #region Task Navigation
        private void CompleteCircuit(object sender, EventArgs e)
        {
            if(isPicking)
                Return();
            else
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }


        private void AddTaskNavigate(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                GlobalItems.CurrentTask = null;
                GlobalItems.RemoveCircuitFromSchedule();
                NavigationService.Navigate(new Uri("/BuildTask.xaml", UriKind.Relative));
            }
        }
        private void NavigateByTaskItem(object sender, SelectionChangedEventArgs e)
        {
            if (!isPlaying)
            {
                //captures navigation event set to -1
                if (taskList.SelectedIndex < 0)
                    return;
                GlobalItems.RemoveCircuitFromSchedule();
                GlobalItems.CurrentTask = GlobalItems.CurrentCircuit.tasks[taskList.SelectedIndex];
                NavigationService.Navigate(new Uri("/BuildTask.xaml", UriKind.Relative));
            }
        }
        #endregion

        #region Circuit Scheduling
        private void PlayCircuit(object sender, EventArgs e)
        {
            GlobalItems.RemoveCircuitFromSchedule();
            if (!isPlaying)
            {
                //app bar handling
                appMenuPlay.IconUri = new Uri("/Toolkit.Content/stopButton-01.png", UriKind.Relative);
                appMenuAdd.IsEnabled = false;
                DateTime first = DateTime.Now;

                foreach (AssignmentTask task in GlobalItems.CurrentCircuit.tasks)
                {
                    first = first.AddHours(task.hours);
                    first = first.AddMinutes(task.minutes);
                    first = first.AddSeconds(task.seconds);

                    Reminder wow = new Reminder(task.name);
                    wow.RecurrenceType = RecurrenceInterval.None;
                    wow.Content = "Next Task: " + task.name;
                    wow.BeginTime = first;
                    wow.ExpirationTime = first;
                    wow.NavigationUri = new Uri("/CircuitBuilder.xaml?index=" + GlobalItems.CurrentCircuitIndex, UriKind.Relative);
                    ScheduledActionService.Add(wow);
                    isPlaying = true;
                }
                LayoutRoot.IsHitTestVisible = false;
                LayoutRoot.Opacity = 0.7;
                GlobalItems.CurrentCircuit.isScheduled = true;
            }
            else
            {
                appMenuPlay.IconUri = new Uri("/Toolkit.Content/nextbutton-01.png", UriKind.Relative);
                isPlaying = false;
                appMenuAdd.IsEnabled = true;
                LayoutRoot.IsHitTestVisible = true;
                LayoutRoot.Opacity = 0.7;
                GlobalItems.CurrentCircuit.isScheduled = false;
                GlobalItems.RemoveCircuitFromSchedule();
            }
        }

       
        #endregion

        #region Overlay Functions
        private void EnterDayPick(object sender, RoutedEventArgs e)
        {
            GlobalItems.RemoveCircuitFromSchedule();
            appMenuAdd.IsEnabled = false;
            appMenuPlay.IsEnabled = false;
            base.Focus();
            this.LayoutRoot.Opacity = 0;
            this.popup.Child = ovr;
            this.popup.IsOpen = true;
            //this.ApplicationBar.IsVisible = true;
            //this.ApplicationBar.IsMenuEnabled = true;
            //appMenuCancel.IsEnabled = true;
            //appMenuAdd.IsEnabled = false;
            SystemTray.IsVisible = false; //to hide system tray
            isPicking = !isPicking;
            //dynamically create a list of LongListMultiSelectorItem
             //foreach (DayofWeek day in GlobalItems.CurrentCircuit.days)
             //{
             //   ovr.circuitDayPicker.ItemsSource.RemoveAt(day.daynum);
             //   ovr.circuitDayPicker.ItemsSource.Insert(day.daynum, day);
             //   System.Diagnostics.Debug.WriteLine(ovr.circuitDayPicker.ItemsSource.Contains(day) + " " + ovr.circuitDayPicker.ItemsSource.Count);
             //   (ovr.circuitDayPicker.ContainerFromItem(day) as LongListMultiSelectorItem).IsSelected = true;
             //}

            
        }
        public void Return()
        {
            appMenuAdd.IsEnabled = true;
            appMenuPlay.IsEnabled = true;
            this.popup.IsOpen = false;
            this.LayoutRoot.Opacity = 1;
            SystemTray.IsVisible = true;
            isPicking = !isPicking;
            GetDayContent();
            if (GlobalItems.CurrentCircuit.isCircuitScheduled)
            {
                appMenuPlay.IsEnabled = false;
                scheduledPanel.Visibility = System.Windows.Visibility.Visible;
            }

            GlobalItems.AddCircuitToSchedule((DateTime)circuitTimePicker.Value);
        }

        //sets list of days to repeat the circuit, and the text of the box
        private void GetDayContent()
        {
            //create dayofweek list
           System.Collections.IList items = ovr.circuitDayPicker.SelectedItems;
           List<DayofWeek> days = new List<DayofWeek>();
           foreach (object item in items)
               days.Add(item as DayofWeek);

           days = GlobalItems.SortDays(days);

           string text = "";

           try
           {
               text += days[0].name.Substring(0, 3);
           }
           catch(ArgumentOutOfRangeException)
           {
               if (ovr.circuitDayPicker.SelectedItems.Count == 0)
                   circuitDayBox.Content = "once";
               
                return;
           }
           if (days.Count > 1)
           {
               text += ", ";
               for (int i = 1; i < days.Count; i++)
               {
                   text += days[i].name.Substring(0,3);
                   if ((i + 2) <= days.Count)
                       text += ", ";
               }
           }
           circuitDayBox.Content = text;

           GlobalItems.CurrentCircuit.setDays(days);
           //GlobalItems.CurrentCircuit.scheduled = text;
           GlobalItems.SaveStorageData(); 
        }
        #endregion

        private void DeleteTaskItem(object sender, RoutedEventArgs e)
        {
            if (taskIndex < 0)
                return;
            GlobalItems.CurrentCircuit.tasks.RemoveAt(taskIndex);
            GlobalItems.CurrentCircuit.setFirstTask();
            taskList.ItemsSource = null;
            taskList.ItemsSource = GlobalItems.CurrentCircuit.tasks;
            GlobalItems.SaveStorageData();
        }

        private void GetSelectedIndexByName(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string taskIndexName = (sender as TextBlock).Text;
            taskIndex = GlobalItems.GetTaskIndexByName(taskIndexName);
        }

        private void HandleTimeChange(object sender, DateTimeValueChangedEventArgs e)
        {
            GlobalItems.RemoveCircuitFromSchedule();
            GlobalItems.AddCircuitToSchedule((DateTime)circuitTimePicker.Value);
        }

    }
}