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

namespace POEStasher
{
    /// <summary>
    /// Interaction logic for EditSyncKey.xaml
    /// </summary>
    public partial class EditSyncKey : Window
    {
        public EditSyncKey()
        {
            InitializeComponent();
            pbKey.Password = Properties.Settings.Default.SyncKey;
            pbKey.Focus();
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SyncKey = pbKey.Password;
            Properties.Settings.Default.Save();
            Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
