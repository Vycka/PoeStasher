using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POEStasher.AccountManager
{
    /// <summary>
    /// Interaction logic for AddEditAccount.xaml
    /// </summary>
    public partial class AddEditAccount : Window
    {
        public bool Changed = false;
        public string Acc, Pass;
        public AddEditAccount(string acc = "", string pass = "")
        {
            InitializeComponent();
            Acc = acc;
            Pass = pass;
            tbAcc.Text = Acc;
            tbPass.Password = Pass;
            tbAcc.Focus();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            Acc = tbAcc.Text;
            Pass = tbPass.Password;
            Changed = true;
            Close();
        }

        private void tbAcc_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableOkButtonIfAllowed();
        }

        private void EnableOkButtonIfAllowed()
        {
            bOk.IsEnabled = ((tbAcc.Text != Acc || tbPass.Password != Pass) && tbAcc.Text != "" && tbPass.Password != "");
        }

        private void tbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EnableOkButtonIfAllowed();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (bOk.IsEnabled)
                if (e.Key == Key.Enter)
                    bOk_Click(this, new RoutedEventArgs());
        }
    }
}
