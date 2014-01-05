using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;

namespace hbreaktest
{
    public partial class TimeOverlay : UserControl
    {
        public TimeOverlay()
        {
            InitializeComponent();
            this.LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
            this.LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;
            
        }

        private void ClearTextSeconds(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            secondsText.Visibility = System.Windows.Visibility.Collapsed;
        }
        
        private void ShowTextSeconds(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            secondsText.Visibility = System.Windows.Visibility.Visible;
        }

        private void ClearTextHours(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            hoursText.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ShowTextHours(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            hoursText.Visibility = System.Windows.Visibility.Visible;
        }

        private void ClearTextMinutes(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            minutesText.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ShowTextMinutes(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            minutesText.Visibility = System.Windows.Visibility.Visible;
        }
        
    }
}
