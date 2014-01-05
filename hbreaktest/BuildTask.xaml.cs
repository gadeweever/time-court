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

namespace hbreaktest
{
    public partial class BuildTask : PhoneApplicationPage
    {
        private Popup popup;
        TimeOverlay ovr;
        public bool isPicking;

        public BuildTask()
        {
            InitializeComponent();
            ovr = new TimeOverlay();
            this.popup = new Popup();
            isPicking = false;
            
        }

        #region NavigationOverrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (GlobalItems.CurrentTask == null)
            {
                ovr.hoursSelector.DataSource.SelectedItem = ovr.secondsSelector.DataSource.SelectedItem = 0 as object;
                taskTimeBox.Content = GetSelectorText();
            }
            else
            {
                taskNameBox.Text = GlobalItems.CurrentTask.name;
                taskFrequencyBox.Text = GlobalItems.CurrentTask.reps.ToString();
                SetSelectors();
                taskTimeBox.Content = GetSelectorText();
            }
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



        private void CancelInput(object sender, EventArgs e)
        {
            if(isPicking)
            {
                Return();
            }
            else
                NavigationService.GoBack();
        }

        private void EnterTimePick(object sender, RoutedEventArgs e)
        {
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
            
        }
       
        public void Return()
        {
            this.popup.IsOpen = false;
            this.LayoutRoot.Opacity = 1;
            SystemTray.IsVisible = true;
            isPicking = !isPicking;

        }

        private void CompleteInput(object sender, EventArgs e)
        {
            if(isPicking)
            {
                taskTimeBox.Content = GetSelectorText();
                Return();
                return;
            }

            if(GlobalItems.CurrentTask == null)
            {
                GlobalItems.CurrentTask = new AssignmentTask(taskNameBox.Text, 
                    Convert.ToInt32(taskFrequencyBox.Text), 
                    Convert.ToInt32(ovr.hoursSelector.DataSource.SelectedItem.ToString()),
                    Convert.ToInt32(ovr.minutesSelector.DataSource.SelectedItem.ToString()),
                    Convert.ToInt32(ovr.secondsSelector.DataSource.SelectedItem.ToString()),
                    GetSelectorText());

                GlobalItems.TaskAdd(GlobalItems.CurrentTask);
            }
            else
            {
                GlobalItems.TaskAdd(new AssignmentTask(taskNameBox.Text,
                    Convert.ToInt32(taskFrequencyBox.Text),
                    Convert.ToInt32(ovr.hoursSelector.DataSource.SelectedItem.ToString()),
                    Convert.ToInt32(ovr.minutesSelector.DataSource.SelectedItem.ToString()),
                    Convert.ToInt32(ovr.secondsSelector.DataSource.SelectedItem.ToString()),
                    GetSelectorText()));
            }
            GlobalItems.SaveStorageData();
            NavigationService.GoBack();
        }


        #region ViewSettingRegion
        private void OnInputPaneShowing(object sender, InputPaneVisibilityEventArgs eventArgs)
        {

            // Don't use framework scroll- or visibility-related 
            // animations that might conflict with the app's logic.
            eventArgs.EnsuredFocusedElementInView = true; 
        
        }

        private void OnInputPaneHiding(object sender, InputPaneVisibilityEventArgs eventArgs)
        {
            eventArgs.EnsuredFocusedElementInView = true; 
        
        }
        #endregion

        #region TextFocusFunctions
        private void DeleteText(object sender, RoutedEventArgs e)
        {
            TextBox a = sender as TextBox;
            a.Text = "";
        }

        private void CheckText(object sender, RoutedEventArgs e)
        {
            if (taskFrequencyBox.Text.Contains('.'))
                taskFrequencyBox.Text = "1";
        }
        private void CheckName(object sender, RoutedEventArgs e)
        {
            TextBox a = sender as TextBox;
            if (a.Text.CompareTo("") == 0)
                a.Text = "New Task";

        }
        #endregion

        #region loopingSelectorFunctions
        private string GetSelectorText()
        {
            int a = Convert.ToInt32(ovr.hoursSelector.DataSource.SelectedItem.ToString());
            int b = Convert.ToInt32(ovr.minutesSelector.DataSource.SelectedItem.ToString());
            int c = Convert.ToInt32(ovr.secondsSelector.DataSource.SelectedItem.ToString());
            string text = "";

            if (a > 0)
            {
                text += a + " hours";
                if (b > 0 && c > 0)
                {
                    text += ", " + b + " minutes, and " + c + " seconds";
                }
                else
                {
                    text += " and " + b + " minutes";
                }

            }
            else if (b > 0)
            {
                text += b + " minutes";
                if (c > 0)
                {
                    text += " and " + c + " seconds";
                }
            }
            else
                text += "No time";

            return text;

        }

        private void SetSelectors()
        {
            ovr.hoursSelector.DataSource.SelectedItem = (object)GlobalItems.CurrentTask.hours;
            ovr.minutesSelector.DataSource.SelectedItem = (object)GlobalItems.CurrentTask.minutes;
            ovr.secondsSelector.DataSource.SelectedItem = (object)GlobalItems.CurrentTask.seconds;
        }

        #endregion

    }
}