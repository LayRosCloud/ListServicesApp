using ApplicationForBD.ApplicationDataBases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ApplicationForBD.Pages
{
    public partial class choiseService : Page
    {
        bool cost = true;
        bool title = default, durationInSeconds = default, title1 = default, desc = false, discount = default;
        private string tabText = "";

        private double maxValue;
        private double minValue;

        //temp Values for sorting list
        private string secondValue = null;
        private string firstValue = null;

        private string temp = null;
        private string queryStr = null;

        private string tempValueItem1;
        private string tempValueItem2;

        private string tempOrder = "Cost";

        private ItemSortCollection items = new ItemSortCollection();
        private ItemSortCollection itemsDesc = new ItemSortCollection();
        private Service serviceObject;
        private Frame frameAdmin;
        List<Service> services = new List<Service>();

        //for slider
        public double MaxValue
        {
            get { return maxValue; }

        }
        public double MinValue
        {
            get { return minValue; }

        }
        public choiseService()
        {
            InitializeObjects();
            InitializeComponent();
        }
        public choiseService(string tabData, Frame frame)
        {
            InitializeObjects();
            InitializeComponent();
            tabText = tabData;
            frameAdmin = frame;
            RefreshListUser(listService, queryStr);

        }
        private void InitializeObjects()
        {
            queryStr += "SELECT * FROM [dbo].[Service]";
            (maxValue, minValue) = FindMaxMinValueDouble("SELECT * FROM [dbo].[Service]");

            tempValueItem1 = firstValue;
            tempValueItem2 = secondValue;
            queryStr = $"Select * FROM [dbo].[Service] WHERE TITLE LIKE '%{temp}%' AND Cost BETWEEN {minValue.ToString().Replace(',', '.')} AND {maxValue.ToString().Replace(',', '.')} /**/ ORDER BY {tempOrder}";
            firstValue = MinValue.ToString();
            secondValue = MaxValue.ToString();
            ComboBoxInitializeItems(items, false);
            ComboBoxInitializeItems(itemsDesc, true, " DESC");

        }

        private void ComboBoxInitializeItems(ItemSortCollection item, bool descSentence, string desc = "")
        {
            item["costComboBox"] = new ItemSort(descSentence, "Cost" + desc);
            item["alphabetComboBox"] = new ItemSort(descSentence, "Title" + desc);
            item["timeComboBox"] = new ItemSort(descSentence, "DurationInSeconds" + desc);
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
        }
        

        private Service AddListService(IDataRecord record) //for record in ListView
            => new Service(record.GetInt32(0), record.GetString(1), record.GetDecimal(2), record.GetInt32(3), Convert.ToString(record.GetValue(4)), record.GetDouble(5), "\\" + record.GetString(6).Trim());
        
        private void RefreshListUser(ListView list, string query) //refresh records in ListView on SQl Query
        {
            try
            {
                services.Clear();
                SqlDataReader reader = AppConnect.GetOpenReader(query);

                while (reader.Read())
                    services.Add(AddListService(reader));

                list.ItemsSource = services.ToArray();

                AppConnect.CloseConnection();
            }
            catch
            {
                
            }

        }
        private (double, double) FindMaxMinValueDouble(string query) //find Max and Min Value on SQL Query
        {
            SqlDataReader reader = AppConnect.GetOpenReader(query);
            double result = default;
            double resultTwo = Double.MaxValue - 5;
            while (reader.Read())
            {
                if (Convert.ToDouble(reader.GetDecimal(2)) > result)
                    result = Convert.ToDouble(reader.GetDecimal(2));

                if (Convert.ToDouble(reader.GetDecimal(2)) < resultTwo)
                    resultTwo = Convert.ToDouble(reader.GetDecimal(2));
            }

            AppConnect.CloseConnection();
            return (result, resultTwo);
        }

        private void SortList()
        {
            
            List<Service> list = new List<Service>();
            list.AddRange(services);
            if (!(findTextBox.Text == "Поиск..." || findTextBox.Text == ""))
                list = SortFindText(list);

            list = SortBetweenCost(list);
            if (discount)
                list = SortDiscount(list);
            list = SortOrderBy(list);
            listService.ItemsSource = list;
        }

        private List<Service> SortFindText(List<Service> reference)
            => (from s in reference where s.Title.Contains(findTextBox.Text) select s).ToList();
        private List<Service> SortBetweenCost(List<Service> reference)
            => (from s in reference
                where Convert.ToDouble(s.Cost) >= betweenSlider.LowerValue
                && Convert.ToDouble(s.Cost) / 100.0 <= betweenSlider.UpperValue
                select s).ToList();
        private List<Service> SortOrderByCost(List<Service> reference)
            => (from s in reference orderby s.DiscountPrice select s).ToList();
        private List<Service> SortOrderByTitle(List<Service> reference)
            => (from s in reference orderby s.Title select s).ToList();
        private List<Service> SortOrderByDurationInSeconds(List<Service> reference)
            => (from s in reference orderby s.Cost select s).ToList();
        private List<Service> SortOrderByCostDesc(List<Service> reference)
            => (from s in reference orderby s.Cost descending select s).ToList();
        private List<Service> SortOrderByTitleDesc(List<Service> reference)
            => (from s in reference orderby s.Title descending select s).ToList();
        private List<Service> SortOrderByDurationInSecondsDesc(List<Service> reference)
            => (from s in reference orderby s.DurationInSeconds descending select s).ToList();
        private List<Service> SortOrderBy(List<Service> reference)
        {
            List<Service> list = new List<Service>();
            if (cost)
                list = desc ? SortOrderByCostDesc(reference) : SortOrderByCost(reference);
            else if (title1)
                list = desc ? SortOrderByTitleDesc(reference) : SortOrderByTitle(reference);
            else if (durationInSeconds)
                list = desc ? SortOrderByDurationInSecondsDesc(reference) : SortOrderByDurationInSeconds(reference);

            return list;
        }
        private List<Service> SortDiscount(List<Service> reference)
            => (from s in reference where s.Discount > 0 select s).ToList();


        private void findTextBox_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e) //Delusion gray text. Kostil.
        {
            if (findTextBox.Text == "Поиск...")
            {
                findTextBox.Text = "";
                temp = findTextBox.Text;
                findTextBox.Focus();
                findTextBox.Foreground = Brushes.Black;
            }

        }


        private void findTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) //SQL Query on Find TextBox
        {
            queryStr = queryStr.Replace($"'%{temp}%'", $"'%{findTextBox.Text}%'");
            temp = findTextBox.Text;
            RefreshListUser(listService, queryStr);
        }

        private void findTextBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (findTextBox.Text == "" || findTextBox.Text == null)
            {
                findTextBox.Text = "Поиск...";
                findTextBox.Foreground = Brushes.Gray;
            }
        }

        private void betweenSlider_FirstSlider(object sender, RoutedEventArgs e) //Slider First Thumb settings
        {
            if (betweenSlider.LowerValue >= betweenSlider.UpperValue)
                betweenSlider.UpperValue = betweenSlider.LowerValue;
            if (betweenSlider.LowerValue <= betweenSlider.Minimum)
                betweenSlider.LowerValue = betweenSlider.Minimum;

            SortList();

        }

        private void betweenSlider_SecondSlider(object sender, RoutedEventArgs e) //Slider second thumb settings
        {
            if (betweenSlider.LowerValue >= betweenSlider.UpperValue)
                betweenSlider.LowerValue = betweenSlider.UpperValue;
            SortList();
        }

        private void costComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboItem = (ComboBoxItem)sender;
            if (orderButton.IsVisible)
                IfDescNoYes(items[comboItem.Name]);

            else if (!orderButton.IsVisible)
                IfDescNoYes(itemsDesc[comboItem.Name]);
        }
        private void IfDescNoYes(ItemSort item) //Method for ComboBox SQL Query
        {
            cost = false;
            title = false;
            durationInSeconds = false;
            desc = item.ContainsDescInSentence;
            switch (item.NameSortText.ToLower())
            {
                case "cost":
                    cost = true;
                    break;
                case "title":
                    title = true;
                    break;
                case "durationinseconds":
                    durationInSeconds = true;
                    break;
            }
            SortList();
        }
        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            VisibleOrderButton(Visibility.Collapsed, Visibility.Visible);
        }
        private void orderDeskButton_Click(object sender, RoutedEventArgs e)
        {
            VisibleOrderButton();
        }

        private void VisibleOrderButton(Visibility visibleOrder = Visibility.Visible, Visibility visibleOrderDESC = Visibility.Collapsed)
        {
            orderButton.Visibility = visibleOrder;
            orderDeskButton.Visibility = visibleOrderDESC;
            desc = !(visibleOrder == Visibility.Visible);
            SortList();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            discount = DiscountCheck.IsChecked == true;
            SortList();
        }
        private void SQLQuery_Click(object sender, RoutedEventArgs e)
        {
            SaveElementFrame.listViewAdminPanel = listService;
            SqlQuery sql = new SqlQuery();
            sql.Show();
            
        }

        private void listService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editBtn.IsEnabled = true;
            delBtn.IsEnabled = true;
            editBtn.Opacity = 1;
            delBtn.Opacity = 1;
            serviceObject = ((ListView)sender).SelectedItem as Service;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            frameAdmin.Navigate(new addDataPage("Service", frameAdmin));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            EditRecord().GetAwaiter();
        }
        private async Task EditRecord()
        {
            if (serviceObject.IsNull)
                return;
            SqlCommand sql = new SqlCommand($@"UPDATE [dbo].[Service] SET Title = '{serviceObject.Title}', 
                                            Cost = {serviceObject.Cost.ToString().Replace(",", ".")}, 
                                            DurationInSeconds = {serviceObject.DurationInSeconds},
                                            description = '{serviceObject.Description}',
                                            discount = {serviceObject.Discount.ToString().Replace(",", ".")}, 
                                            MainImagePath = '{serviceObject.MainImagePath.Remove(0, 1)}' 
                                            WHERE id = {serviceObject.Id}", AppConnect.GetConnection);
            AppConnect.OpenConnectionAsync();
            if (await sql.ExecuteNonQueryAsync() == 1)
                CompleteTextAnimation("Данные записаны");
            
            else
                CompleteTextAnimation("Данные не записаны");
            
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord();
        }
        private void DeleteRecord()
        {
            if (serviceObject.IsNull)
                return;

            SqlCommand sql = AppConnect.GetOpenSqlCommand($"DELETE FROM [dbo].[Service] WHERE Id = {serviceObject.Id}");
            if (sql.ExecuteNonQuery() == 1)
                CompleteTextAnimation("Данные удалены");
            else
                CompleteTextAnimation("Данные не удалены");
            
            AppConnect.CloseConnection();
            RefreshListUser(listService, queryStr);
            SortList();
            (maxValue, minValue) = FindMaxMinValueDouble("SELECT * FROM [dbo].[Service]");

        }
        private void CompleteTextAnimation(string textComplete)
        {
            CompleteText.Text = textComplete;
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = true;
            CompleteText.BeginAnimation(OpacityProperty, doubleAnimation);
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshListUser(listService, queryStr);
            (maxValue, minValue) = FindMaxMinValueDouble("SELECT * FROM [dbo].[Service]");
        }
    }
}
