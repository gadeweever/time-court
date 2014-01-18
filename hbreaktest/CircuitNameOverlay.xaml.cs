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
    public partial class CircuitNameOverlay : UserControl
    {
        public CircuitNameOverlay()
        {
            InitializeComponent();
            this.LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
            this.LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;

            nameUseText.Text = "this is the name of the task list used to identify your task!";
        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            TextBox a = sender as TextBox;
            a.Text = "";
        }

        private void CheckName(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.CompareTo("") == 0)
                (sender as TextBox).Text = "New Circuit";
        }

        
    }
}
