using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using ApplicationForBD.ApplicationDataBases;

namespace ApplicationForBD.Pages
{
    public partial class ServicesPage : Page
    {
        private bool cost = true;
        private bool title = default;
        private bool durationInSeconds = default;
        private bool discount = default;

        private bool desc = false;

        private double maxValue;
        private double minValue;
        
        //temp Values for sorting list
        string secondValue = null;
        string firstValue = null;

        string temp = null;
        string queryStr = null;

        string tempValueItem1;
        string tempValueItem2;


        ItemSortCollection items = new ItemSortCollection();
        ItemSortCollection itemsDesc = new ItemSortCollection();
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

        public ServicesPage()
        {
            InitializeObjects();
            InitializeComponent();
            RefreshListUser(listUser, queryStr);
            
            borderCount.Visibility = SaveElementFrame.listService.Count == 0? Visibility.Collapsed: Visibility.Visible;
            countListService.Text = SaveElementFrame.listService.Count.ToString();
        }

        /********
         * Инициализация объектов
         *********/
        private void InitializeObjects()
        {
            (maxValue, minValue) = FindMaxMinValueDouble("SELECT * FROM [dbo].[Service]");
            
            tempValueItem1 = firstValue;
            tempValueItem2 = secondValue;
            
            queryStr = $"Select * FROM [dbo].[Service] WHERE TITLE LIKE '%{temp}%' AND (Cost - (Cost * Discount)) BETWEEN {minValue.ToString().Replace(',', '.')} AND {maxValue.ToString().Replace(',', '.')} /**/ ORDER BY (Cost - (Cost * Discount))";
            
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
        /********
         * Методы для инициализации List View и поиска максимального и минимального значения.
         *********/
        private Service AddListService(IDataRecord record) //for record in ListView
        {
            return new Service(record.GetInt32(0), record.GetString(1), record.GetDecimal(2), 
                record.GetInt32(3) / 60.0, Convert.ToString(record.GetValue(4)), 
                record.GetDouble(5) * 100, "\\" + record.GetString(6).Trim());
            

        }
        private void RefreshListUser(ListView list, string query) //refresh records in ListView on SQl Query
        {
            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);
            AppConnect.OpenConnectionAsync();
            SqlDataReader reader = sql.ExecuteReader();

            while (reader.Read())
                services.Add(AddListService(reader));

            list.ItemsSource = services.ToArray();
            AppConnect.CloseConnection();

        }
        private (double, double) FindMaxMinValueDouble(string query) //find Max and Min Value on SQL Query
        {
            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);

            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();
            double result = Double.MinValue;
            double resultTwo = Double.MaxValue - 5;
            while (reader.Read())
            {
                double p = Convert.ToDouble(reader.GetDecimal(2));

                if ((p - p * reader.GetDouble(5)) > result)
                {
                    result = p - p * reader.GetDouble(5);
                }

                else if ((p - p * reader.GetDouble(5)) < resultTwo)
                {
                    resultTwo = p - p * reader.GetDouble(5);
                }
            }

            AppConnect.CloseConnection();
            return (result, resultTwo);
        }

        /********
          * Сортировка поиск
          *********/
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

            listUser.ItemsSource = list;
        }

        private List<Service> SortFindText(List<Service> reference) 
            =>(from s in reference where s.Title.Contains(findTextBox.Text) select s).ToList();
        private List<Service> SortBetweenCost(List<Service> reference)
            => (from s in reference where Convert.ToDouble(s.Cost) - Convert.ToDouble(s.Cost) * s.Discount/100.0 >= betweenSlider.LowerValue 
                && Convert.ToDouble(s.Cost) - Convert.ToDouble(s.Cost) * s.Discount / 100.0 <= betweenSlider.UpperValue select s).ToList();
        private List<Service> SortOrderByCost(List<Service> reference)
            => (from s in reference orderby s.DiscountPrice select s).ToList();
        private List<Service> SortOrderByTitle(List<Service> reference)
            => (from s in reference orderby s.Title select s).ToList();
        private List<Service> SortOrderByDurationInSeconds(List<Service> reference)
            => (from s in reference orderby s.DurationInSeconds select s).ToList();
        private List<Service> SortOrderByCostDesc(List<Service> reference)
            => (from s in reference orderby s.DiscountPrice descending select s).ToList();
        private List<Service> SortOrderByTitleDesc(List<Service> reference)
            => (from s in reference orderby s.Title descending select s).ToList();
        private List<Service> SortOrderByDurationInSecondsDesc(List<Service> reference)
            => (from s in reference orderby s.DurationInSeconds descending select s).ToList();
        private List<Service> SortOrderBy(List<Service> reference)
        {
            List<Service> list = new List<Service>();
            if (cost)
                list = desc ? SortOrderByCostDesc(reference) : SortOrderByCost(reference);
            else if (title)
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
            SortList();
        }

        private void findTextBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (findTextBox.Text == "" || findTextBox.Text == null)
            {
                findTextBox.Text = "Поиск...";
                findTextBox.Foreground = Brushes.Gray;
            }
        }
        /********
         * Сортировка ползунок
         *********/
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
        /********
         * Сортировка ComboBox
         *********/
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
        /********
         * Сортировка кнопка по возрастанию, убыванию
         *********/
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
        /********
         * Сортировка кнопка Only/not only discount
         *********/
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            discount = DiscountCheck.IsChecked == true;
            SortList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel grid = ((((Button)sender).Parent as StackPanel).Parent as Grid).Children[2] as StackPanel;
            TextBlock text = grid.Children[0] as TextBlock;
            SqlCommand sql = new SqlCommand($"Select * from [dbo].[Service] WHERE id = {text.Text}", AppConnect.GetConnection);
            Service serv = null;
            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();
            reader.Read();
            serv = AddListService(reader);

            Service s = (from a in SaveElementFrame.listService where a.Id == Convert.ToInt32(text.Text) select a).FirstOrDefault();

            if (s != null)
            {
                SaveElementFrame.listService.Remove(s);
                borderCount.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
                countListService.Text = SaveElementFrame.listService.Count.ToString();
                ((Button)sender).Content = "В корзину";
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(185, 255, 38));
                return;
            }
            
            ((Button)sender).Content = "В корзине";
            SaveElementFrame.listService.Add(serv);
            borderCount.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            countListService.Text = SaveElementFrame.listService.Count.ToString();
            ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(218, 236, 182));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveElementFrame.frameHub.Navigate(new ShoppingCardPage());
        }
    }
}
