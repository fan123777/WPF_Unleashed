﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Unleashed._4_ProfessionalTools._12_Resources
{
    /// <summary>
    /// Interaction logic for LogicalResources.xaml
    /// </summary>
    public partial class LogicalResources : Window
    {
        public LogicalResources()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimpleWindow window = new SimpleWindow();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LogicResource window = new LogicResource();
            window.Show();
        }
    }
}
