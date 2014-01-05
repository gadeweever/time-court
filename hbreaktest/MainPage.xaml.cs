using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using hbreaktest.Resources;
using System.Windows.Controls.Primitives;
using System.IO.IsolatedStorage;
using System.IO;
using System.Threading;

namespace hbreaktest
{
    public partial class MainPage : PhoneApplicationPage
    {

        private Popup popup;
        CircuitNameOverlay ovr;
        public bool isPicking;
        private int circuitIndex;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ovr = new CircuitNameOverlay();
            this.popup = new Popup();
            isPicking = false;
            GlobalItems.AppCircuits = new List<Assignment>();
            LoadStoredData();
            //set to -2 because default selectedIndex is -1, and 0 is non-informative
            circuitIndex = -2;

        }

        private void NewCircuit(object sender, EventArgs e)
        {
            if (!isPicking)
            {
                if(ovr.circuitNameBox.Text.CompareTo("New Circuit") != 0)
                    ovr.circuitNameBox.Text = "";
                isPicking = !isPicking;
                //set visible properties of the overlay
                this.LayoutRoot.Opacity = 1;
                this.popup.Child = ovr;
                this.popup.Opacity = .8;
                this.popup.IsOpen = true;
                SystemTray.IsVisible = false; //to hide system tray
            }

            else
            {
                if(GlobalItems.GetCircuitIndexByName(ovr.circuitNameBox.Text) != -1)
                {
                    MessageBox.Show("You already have a circuit with the same name!");
                    return;
                }

                GlobalItems.AppCircuits.Add(new Assignment(ovr.circuitNameBox.Text));
                GlobalItems.CurrentCircuitIndex = GlobalItems.AppCircuits.Count - 1;
                GlobalItems.CurrentCircuit = GlobalItems.AppCircuits[GlobalItems.CurrentCircuitIndex];

                //get info from radios
                if ((bool)ovr.scheduleRadio.IsChecked)
                    GlobalItems.CurrentCircuit.isCircuitScheduled = true;
                else
                    GlobalItems.CurrentCircuit.isCircuitScheduled = false;

                //reset menu list to the stored assignments   
                base.Focus();
                circuitList.ItemsSource = null;
                circuitList.ItemsSource = GlobalItems.AppCircuits;

                GlobalItems.SaveStorageData();
                Return();
                NavigationService.Navigate(new Uri("/CircuitBuilder.xaml", UriKind.Relative));
                
            
            }
        }

        #region Navigation Overrides
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (isPicking)
            {
                e.Cancel = true;
                Return();
            }
        }

        //OnNavigatedTo sets the circuitList selection to -1 for visual purposes
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            circuitList.SelectedIndex = -1;
        }
        #endregion

        public void Return()
        {
            this.popup.IsOpen = false;
            this.LayoutRoot.Opacity = 1;
            SystemTray.IsVisible = true;
            isPicking = !isPicking;

        }
       
        #region IsolatedStorageHandling
        

         private int LoadStoredData()
         {
             using (var filesystem = IsolatedStorageFile.GetUserStoreForApplication())
             {
                 if (!filesystem.FileExists("assignments.dat"))
                 {
                     return 1;                
                 }
                 else
                 {
                     using (var fs = new IsolatedStorageFileStream(
                      "assignments.dat", FileMode.Open, filesystem))
                     {
                         var serializer = new System.Runtime.Serialization
                           .Json.DataContractJsonSerializer(typeof(
                           List<Assignment>));
                         GlobalItems.AppCircuits = serializer.ReadObject(fs) as
                           List<Assignment>;
                     }
                     //reset menu list to the stored assignments
                     circuitList.ItemsSource = null;
                     circuitList.ItemsSource = GlobalItems.AppCircuits;
                     
                 }
             }
             return 0;

         }
        #endregion

         private void NavigateToCircuit(object sender, SelectionChangedEventArgs e)
         {
             //captures navigation event set to -1
             if (circuitList.SelectedIndex < 0)
                 return;
             GlobalItems.CurrentCircuitIndex = circuitList.SelectedIndex;
             GlobalItems.CurrentCircuit = GlobalItems.AppCircuits[GlobalItems.CurrentCircuitIndex];
             NavigationService.Navigate(new Uri("/CircuitBuilder.xaml", UriKind.Relative));
         }

        
         

         private void DeleteCircuitItem(object sender, RoutedEventArgs e)
         {
             if (circuitIndex < 0)
                 return;
             GlobalItems.AppCircuits.RemoveAt(circuitIndex);
             GlobalItems.CurrentCircuit = GlobalItems.AppCircuits[circuitIndex];
             GlobalItems.RemoveCircuitFromSchedule();
             circuitList.ItemsSource = null;
             circuitList.ItemsSource = GlobalItems.AppCircuits;
             GlobalItems.SaveStorageData();
         }

         private void GetSelectedIndexByName(object sender, System.Windows.Input.GestureEventArgs e)
         {
             string circuitIndexName = (sender as TextBlock).Text;
             circuitIndex = GlobalItems.GetCircuitIndexByName(circuitIndexName);
             
         }

         #region Toggle Switch Functions

         private void SetCircuitScheduled(object sender, RoutedEventArgs e)
         {
             DetermineToggle(sender, 1);
         }

         private void UnsetCircuitScheduled(object sender, RoutedEventArgs e)
         {
             DetermineToggle(sender, 0);
         }

        //takes a toggle button and an int to determine how to set the isScheduled property of the current circuit
        // a zero is false or isChecked = false, one: isChecked = true; the property is set, and data is saved
         private void DetermineToggle(object sender, int check)
         {
             ToggleSwitch toggle = sender as ToggleSwitch;
             Grid selectedGrid = (sender as ToggleSwitch).Parent as Grid;
             TextBlock circuitName = (selectedGrid.Children[0] as StackPanel).Children[0] as TextBlock;
             circuitIndex = GlobalItems.GetCircuitIndexByName(circuitName.Text);
             GlobalItems.CurrentCircuit = GlobalItems.AppCircuits[circuitIndex];
             GlobalItems.CurrentCircuitIndex = circuitIndex;

             switch (check)
             {
                 case 0: GlobalItems.CurrentCircuit.isCircuitScheduled = false;
                     GlobalItems.RemoveCircuitFromSchedule();
                     break;
                 default: GlobalItems.CurrentCircuit.isCircuitScheduled = true;
                     break;
             }

             //disables the button while data is being saved. Avoids an idiot back and forth toggling
             toggle.IsEnabled = false;
             GlobalItems.SaveStorageData();
             toggle.IsEnabled = true;
         }

         #endregion

    }
}