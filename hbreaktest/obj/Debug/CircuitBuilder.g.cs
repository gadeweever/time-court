﻿#pragma checksum "C:\Users\Gamal\Documents\GitHub\time-court\hbreaktest\CircuitBuilder.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2A343F0E6FA67ADDA697AA2D3489DA8A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace hbreaktest {
    
    
    public partial class CircuitBuilder : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock circuitTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.StackPanel scheduledPanel;
        
        internal Microsoft.Phone.Controls.TimePicker circuitTimePicker;
        
        internal System.Windows.Controls.Button circuitDayBox;
        
        internal System.Windows.Controls.StackPanel CurrentTaskPanel;
        
        internal System.Windows.Controls.TextBlock currentTaskName;
        
        internal System.Windows.Controls.ListBox taskList;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/hbreaktest;component/CircuitBuilder.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.circuitTitle = ((System.Windows.Controls.TextBlock)(this.FindName("circuitTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.scheduledPanel = ((System.Windows.Controls.StackPanel)(this.FindName("scheduledPanel")));
            this.circuitTimePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("circuitTimePicker")));
            this.circuitDayBox = ((System.Windows.Controls.Button)(this.FindName("circuitDayBox")));
            this.CurrentTaskPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrentTaskPanel")));
            this.currentTaskName = ((System.Windows.Controls.TextBlock)(this.FindName("currentTaskName")));
            this.taskList = ((System.Windows.Controls.ListBox)(this.FindName("taskList")));
        }
    }
}

