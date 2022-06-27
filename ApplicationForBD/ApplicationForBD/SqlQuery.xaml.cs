using ApplicationForBD.ApplicationDataBases;
using ApplicationForBD.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationForBD
{
    /// <summary>
    /// Логика взаимодействия для SqlQuery.xaml
    /// </summary>
    public partial class SqlQuery : Window
    {
        public SqlQuery()
        {
            InitializeComponent();
            richTextSQL.Text = SaveElementFrame.SQLQuery;
        }

        private void richTextSQL_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveElementFrame.SQLQuery = richTextSQL.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshListUser(SaveElementFrame.listViewAdminPanel, SaveElementFrame.SQLQuery);
            }
            catch(Exception ex)
            {
                richTextSQL.Text += Environment.NewLine + $"/*Ошибка! {ex.Message}*/";
            }
        }
        private Service AddListService(IDataRecord record) //for record in ListView
        {
            return new Service(record.GetInt32(0), record.GetString(1), record.GetDecimal(2), record.GetInt32(3), "описания нет...", record.GetDouble(5), "\\" + record.GetString(6).Trim());


        }
        public void RefreshListUser(ListView list, string query) //refresh records in ListView on SQl Query
        {

            List<Service> services = new List<Service>();
            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);

            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();

            while (reader.Read())
                services.Add(AddListService(reader));

            list.ItemsSource = services.ToArray();

            AppConnect.CloseConnection();

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            richTextSQL.Text = "";
            comboFile.SelectedIndex = -1;

        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "SQL-Query (*.sql)|*.sql";

            if (open.ShowDialog() == true)
            {
                StreamReader stream = new StreamReader(open.OpenFile());
                richTextSQL.Text = stream.ReadToEnd();
            }
            comboFile.SelectedIndex = -1;

        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            comboFile.SelectedIndex = -1;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "SQL-Query (*.sql)|*.sql";
            if (save.ShowDialog() == true)
            {
                StreamWriter stream = new StreamWriter(save.FileName);
                stream.WriteLine(richTextSQL.Text);
            }
            comboFile.SelectedIndex = -1;

        }
    }
}
