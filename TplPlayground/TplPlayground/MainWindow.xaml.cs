﻿using System.ComponentModel.Composition;
using System.Windows;

namespace TplPlayground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(MainWindow))]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
