using ApplicationForBD.ApplicationDataBases;
using ApplicationForBD.ModelTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для TablesPage.xaml
    /// </summary>
    public partial class TablesPage : Page
    {
        TabItem itemTab;
        ulong id = 0;
        TabControl tabControl;
        List<TabItem> tabItems;
        Frame frameTable;
        public TablesPage()
        {
            InitializeComponent();
            RefreshListTables(listTables, "SELECT name, create_date, modify_date FROM sys.tables");
        }
        public TablesPage(TabItem item, ulong id, TabControl tab, List<TabItem> tabs, Frame frame)
        {
            InitializeComponent();
            RefreshListTables(listTables, "SELECT name, create_date, modify_date FROM sys.tables");
            itemTab = item;
            this.id = id;
            tabControl = tab;
            tabItems = tabs;
            frameTable = frame;
        }
        private void RefreshListTables(ListView list, string query)
        {
            List<TablesLang> services = new List<TablesLang>();
            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);

            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString(0) == "sysdiagrams")
                    continue;
                services.Add(ReturnTablesLang(reader));
            }

            list.ItemsSource = services.ToArray();
            AppConnect.CloseConnection();
        }
        private TablesLang ReturnTablesLang(IDataRecord record)
        {
            return new TablesLang(record.GetString(0), record.GetDateTime(1), record.GetDateTime(2));
        }


        private void listTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = sender as ListView;


            switch ((list.SelectedItem as TablesLang).NameTable)
            {
                case "Service":
                    frameTable.Navigate(new choiseService("[dbo].[Service]", frameTable));
                    itemTab.Header = HeaderTabItem("[dbo].[Service]");
                    break;
                default:
                    frameTable.Navigate(new AdaptiveTablePage(frameTable, (list.SelectedItem as TablesLang).NameTable));
                    itemTab.Header = HeaderTabItem($"[dbo].[{(list.SelectedItem as TablesLang).NameTable}]");
                    break;
            }
        }
        private StackPanel HeaderTabItem(string headerText)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock nameText = new TextBlock();
            Button close = new Button();
            nameText.Text = headerText;
            nameText.Foreground = Brushes.Black;
            nameText.HorizontalAlignment = HorizontalAlignment.Center;
            nameText.Width = 100;

            close.Background = Brushes.Transparent;
            close.Foreground = Brushes.Blue;
            close.Content = "X";
            close.Name = "CLose_" + id.ToString();
            close.BorderThickness = new Thickness(0);
            close.BorderBrush = Brushes.Transparent;
            close.Click += Close_Click;

            stackPanel.Background = Brushes.Transparent;
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(nameText);
            stackPanel.Children.Add(close);

            return stackPanel;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                tabControl.Items.Remove(tabItems[Convert.ToInt32(btn.Name.Split('_')[1]) - 1]);
            }
        }

        private void findTextBox_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (findTextBox.Text == "Поиск...")
            {
                findTextBox.Text = "";
                findTextBox.Foreground = Brushes.Black;
            }
        }

        private void findTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListTables(listTables, $"SELECT name, create_date, modify_date FROM sys.tables WHERE name LIKE '%{findTextBox.Text}%'");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frameTable.Navigate(new ClientsNowPage());
        }
    }
}
