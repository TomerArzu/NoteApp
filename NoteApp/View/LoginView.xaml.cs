﻿using NoteApp.ViewModel;
using System;
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

namespace NoteApp.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            LoginViewModel vm = new LoginViewModel();
            GridContainer.DataContext = vm;
            vm.HasLoggedin += Vm_HasLoggedin;
        }

        private void Vm_HasLoggedin(object sender, EventArgs e)
        {
            this.Close();
        }

        private void haveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterSP.Visibility = Visibility.Collapsed;
            LoginSP.Visibility = Visibility.Visible;
        }

        private void noAccountButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterSP.Visibility = Visibility.Visible;
            LoginSP.Visibility = Visibility.Collapsed;
        }
    }
}
