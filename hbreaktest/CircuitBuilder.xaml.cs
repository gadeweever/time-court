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
        ApplicationBarIconButton appMenuSave;
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
            appMenuSave = (ApplicationBarIconButton)this.ApplicationBar.Buttons[1];
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

            ////handle visiblility
            //if (GlobalItems.CurrentCircuit.isCircuitScheduled)
            //{
            //    System.Diagnostics.Debug.WriteLine(ContentPanel.ActualHeight + " " + scheduledPanel.ActualHeight + " " + taskList.MaxHeight);
            //    appMenuPlay.IsEnabled = false;
            //    scheduledPanel.Visibility = System.Windows.Visibility.Visible;
            //    taskList.Height = Application.Current.Host.Content.ActualHeight * 0.4275;
            //}
            //else
            //    taskList.Height = Application.Current.Host.Content.ActualHeight * 0.6625;

            
            int j = 0;
            //System.Diagnostics.Debug.WriteLine(GlobalItems.CurrentCircuit.name + " " + GlobalItems.CurrentCircuit.days.Count);
            if (j == GlobalItems.CurrentCircuit.days.Count)
                circuitDayBox.Content = "once";
            CheckTimeForUI();
            
        }

        
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (isPicking)
            {
                e.Cancel = true;
                Return();
            }
            else
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                GlobalItems.SaveStorageData();
            }
        }
        #endregion

        #region Task Navigation
        private void CompleteCircuit(object sender, EventArgs e)
        {
            if (isPicking)
                Return();
            else
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                GlobalItems.SaveStorageData();
            }
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
            if (!GlobalItems.CurrentCircuit.isScheduled)
            {
                DisableUI();
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
                    GlobalItems.CurrentCircuit.times.Add(first);
                    
                }
                currentTaskName.Text = GlobalItems.CurrentCircuit.tasks[0].name;
                GlobalItems.CurrentCircuit.isScheduled = true;
            }
            else
            {
                EnableUI();
                GlobalItems.CurrentCircuit.isScheduled = false;
                GlobalItems.RemoveCircuitFromSchedule();
            }
            
        }

        private void CheckScheduleDone()
        {
            if (GlobalItems.CurrentCircuit.isScheduled)
            {
                DateTime now = DateTime.Now;
                if (now.CompareTo(GlobalItems.CurrentCircuit.times[GlobalItems.CurrentCircuit.times.Count - 1]) < 0)
                   GlobalItems.CurrentCircuit.isScheduled = true;
                else
                    GlobalItems.CurrentCircuit.isScheduled =  false;
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
            SystemTray.IsVisible = false; //to hide system tray
            isPicking = !isPicking;

            
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

        #region UI Control
        private void CheckTimeForUI()
        {
            CheckScheduleDone();
            if (GlobalItems.CurrentCircuit.isScheduled)
            {
                //handle visibility
                DisableUI();

                int length = GlobalItems.CurrentCircuit.tasks.Count;
                DateTime now = DateTime.Now;
                currentTaskName.Text = GlobalItems.CurrentCircuit.tasks[0].name;
                for (int i = 0; i < length; i++)
                {
                    if (GlobalItems.CurrentCircuit.times[i].CompareTo(now) > 0)
                        return;
                    currentTaskName.Text = GlobalItems.CurrentCircuit.tasks[i].name;
                }
            }
            else
            {
                EnableUI();
                currentTaskName.Text = "none";
            }
        }

        private void DisableUI()
        {
            appMenuPlay.IconUri = new Uri("/Toolkit.Content/stopButton-01.png", UriKind.Relative);
            appMenuPlay.Text = "stop";
            appMenuAdd.IsEnabled = false;
            appMenuSave.IsEnabled = false;
            LayoutRoot.IsHitTestVisible = false;
            LayoutRoot.Opacity = 0.7;
        }

        private void EnableUI()
        {
            appMenuPlay.IconUri = new Uri("/Toolkit.Content/nextbutton-01.png", UriKind.Relative);
            appMenuPlay.Text = "play";
            appMenuAdd.IsEnabled = true;
            appMenuSave.IsEnabled = true;
            LayoutRoot.IsHitTestVisible = true;
            LayoutRoot.Opacity = 1;
        }

        #endregion



    }
}