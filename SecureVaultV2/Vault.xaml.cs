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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SecureVaultV2
{
    /// <summary>
    /// Interaction logic for Vault.xaml
    /// </summary>
    public partial class Vault : Window
    {
        public Vault()
        {
            InitializeComponent();
            ShowMe();
        }
        public void ShowMe()
        {
            (FindResource("showMe") as Storyboard).Begin(this);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

     
    }
}
