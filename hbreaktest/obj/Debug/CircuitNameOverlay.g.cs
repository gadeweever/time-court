﻿#pragma checksum "C:\Users\Gamal\Documents\GitHub\time-court\hbreaktest\CircuitNameOverlay.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5B7CA54BCC2EC69218FC58C872FD4920"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    public partial class CircuitNameOverlay : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock timeOverlayTitle;
        
        internal System.Windows.Controls.Grid curcuitOverlayGrid;
        
        internal System.Windows.Controls.TextBox circuitNameBox;
        
        internal System.Windows.Controls.TextBlock nameUseText;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/hbreaktest;component/CircuitNameOverlay.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.timeOverlayTitle = ((System.Windows.Controls.TextBlock)(this.FindName("timeOverlayTitle")));
            this.curcuitOverlayGrid = ((System.Windows.Controls.Grid)(this.FindName("curcuitOverlayGrid")));
            this.circuitNameBox = ((System.Windows.Controls.TextBox)(this.FindName("circuitNameBox")));
            this.nameUseText = ((System.Windows.Controls.TextBlock)(this.FindName("nameUseText")));
        }
    }
}

