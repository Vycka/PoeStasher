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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POEStasher.ItemTree
{
    /// <summary>
    /// Interaction logic for ItemTree.xaml
    /// </summary>
    public partial class ItemTree : UserControl
    {
        public ItemTree()
        {
            InitializeComponent();
        }
        public void SetDataSource(CategorizedItemsList cil)
        {
            treeView1.ItemsSource = cil.ClassCatList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }

        private void treeView1_Expanding(object sender, RoutedEventArgs e)
        {
        }
    }
}
