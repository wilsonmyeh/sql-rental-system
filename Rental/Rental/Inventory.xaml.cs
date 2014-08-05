﻿using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace Rental
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        private SQLiteHelper helper;
        private DataTable table;

        public Inventory()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate //Clock
            {
                this.clock.Text = DateTime.Now.ToString();
            }, this.Dispatcher);

            helper = new SQLiteHelper();
            table = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT types.name AS Type, series.title AS Title, series.ongoing AS 疵, series.artist AS Author, series.publisher AS Publisher, series.reference AS Reference " + 
                "FROM series, types " + 
                "WHERE types.typeId=series.typeId");
            gridInv.DataContext = table.DefaultView;

        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            NewSeries win = new NewSeries();
            win.Show();
        }

        private void search_KeyDown(object sender, KeyEventArgs e) //Dynamically updating search results
        {
            table = helper.GetDataTable(
                "SELECT types.name AS Type, series.title AS Title, series.ongoing AS 疵, series.artist AS Author, series.publisher AS Publisher, series.reference AS Reference " + 
                "FROM series, types " + 
                "WHERE types.typeId=series.typeId AND (customers.code=" + search.Text + " OR customers.name=" + search.Text + ")");
        }
    }
}
