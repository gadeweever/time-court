﻿#pragma checksum "C:\Users\Gamal\Documents\GitHub\hbreaktest\hbreaktest\BuildTask.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "16EE770A47BC775AA238612D0439C91D"
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
using Microsoft.Phone.Shell;
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
    
    
    public partial class BuildTask : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox taskNameBox;
        
        internal System.Windows.Controls.Button taskTimeBox;
        
        internal System.Windows.Controls.TextBox taskFrequencyBox;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton cancelButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/hbreaktest;component/BuildTask.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.taskNameBox = ((System.Windows.Controls.TextBox)(this.FindName("taskNameBox")));
            this.taskTimeBox = ((System.Windows.Controls.Button)(this.FindName("taskTimeBox")));
            this.taskFrequencyBox = ((System.Windows.Controls.TextBox)(this.FindName("taskFrequencyBox")));
            this.cancelButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("cancelButton")));
        }
    }
}

