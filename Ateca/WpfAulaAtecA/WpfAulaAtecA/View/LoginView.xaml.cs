﻿using WpfAulaAtecA.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAulaAtecA.View
{
    
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordChangedHandler(object sender, RoutedEventArgs e)
        {
                if (DataContext is LoginViewModel vm && sender is PasswordBox pb)
            {
                vm.Password=pb.Password;
            }
        }
    }
}
