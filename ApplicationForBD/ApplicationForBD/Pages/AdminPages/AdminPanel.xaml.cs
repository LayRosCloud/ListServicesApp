using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        ulong id = 0;
        ulong idTwo = 0;
        List<TabItem> tabItems = new List<TabItem>();
        public AdminPanel()
        {
            InitializeComponent();
            
            tabControl.Items.Add(CreateTabItem("Новая вкладка"));
            tabControl.Items.Add(CreatePlusTabItem());

            
        }
        public TabItem CreateTabItem(string headerText)
        {
            Frame frame = CreateFrame();
            TabItem tabItem = TabItemSetting(frame);

            id++;
            tabItems.Add(tabItem);
            SaveElementFrame.frameAdmin.Navigate(new TablesPage(tabItem, id, tabControl, tabItems, frame));
            return tabItem;
        }

        private TabItem TabItemSetting(Frame frame)
        {
            TabItem tabItem = new TabItem();
            tabItem.Background = Brushes.Transparent;
            tabItem.BorderThickness = new Thickness(0);
            tabItem.Header = HeaderTabItem("новая вкладка");
            tabItem.Name = "TabItem_" + id.ToString();
            tabItem.IsEnabled = true;
            tabItem.IsSelected = true;
            tabItem.Content = frame;
            tabItem.AllowDrop = true;
            return tabItem;
        }

        private StackPanel HeaderTabItem(string headerText)
            => ReturnStackPanelSetting(ButtonSetting(), TextBlockSetting(headerText));

        private Button ButtonSetting()
        {
            Button but = new Button();
            but.Background = Brushes.Transparent;
            but.Foreground = Brushes.Blue;
            but.Content = "X";
            but.Name = "CLose_" + id.ToString();
            but.BorderThickness = new Thickness(0);
            but.BorderBrush = Brushes.Transparent;
            but.Click += Close_Click;
            return but;
        }
        private TextBlock TextBlockSetting(string headerText)
        {
            TextBlock text = new TextBlock();
            text.Text = headerText;
            text.Foreground = Brushes.Black;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.Width = 100;
            return text;
        }
        
        private StackPanel ReturnStackPanelSetting(Button but, TextBlock text)
        {
            StackPanel stack = new StackPanel();
            stack.Background = Brushes.Transparent;
            stack.Orientation = Orientation.Horizontal;
            stack.Children.Add(text);
            stack.Children.Add(but);
            return stack;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                tabControl.Items.Remove(tabItems[Convert.ToInt32(btn.Name.Split('_')[1])]);
            }
        }

        private Frame CreateFrame()
        {
            Frame frame = new Frame();
            frame.Name = "Frame_"+id.ToString();
            SaveElementFrame.frameAdmin = frame;
            return frame;
        }

        public TabItem CreatePlusTabItem()
        {
            TabItem tabItem = new TabItem();

            tabItem.Background = Brushes.White;
            tabItem.BorderThickness = new Thickness(0);
            tabItem.Header = BtnPlusSetting();
            tabItem.Name = "plusObject_" + idTwo.ToString();
            tabItem.IsSelected = false;
            tabItem.Content = ReturnGrid(ButtonSettingPlus());

            tabItem.MouseUp += TabItem_MouseUp;
            tabItem.GotFocus += TabItem_GotFocus;
            idTwo++;

            return tabItem;
        }
        private TextBlock BtnPlusSetting()
        {
            TextBlock btnPlus = new TextBlock();
            btnPlus.Background = Brushes.Transparent;
            btnPlus.Foreground = Brushes.Blue;
            btnPlus.Width = 10;
            btnPlus.Margin = new Thickness(0);
            btnPlus.Text = "+";
            btnPlus.HorizontalAlignment = HorizontalAlignment.Center;
            return btnPlus;
        }
        private Grid ReturnGrid(Button btnCreate)
        {
            Grid grid = new Grid();
            grid.Children.Add(btnCreate);
            return grid;
        }
        private Button ButtonSettingPlus()
        {
            Button btnCreate = new Button();
            btnCreate.Background = Brushes.Transparent;
            btnCreate.Foreground = Brushes.White;
            btnCreate.FontSize = 80;
            btnCreate.Margin = new Thickness(0);
            btnCreate.BorderThickness = new Thickness(0);
            btnCreate.Click += BtnCreate_Click;
            btnCreate.Content = "Создать вкладку...";
            return btnCreate;
        }


        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {

            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(6));
            TabItem tab = sender as TabItem;
            Grid grid = tab.Content as Grid;
            grid.Children[0].BeginAnimation(OpacityProperty, animation);
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = CreateTabItem("Новая вкладка");
            tabControl.Items.Remove(tabControl.Items[tabControl.Items.Count - 1]);
            tabControl.Items.Add(tab);
            tabControl.Items.Add(CreatePlusTabItem());
        }

        private void TabItem_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TabItem tab = CreateTabItem("Новая вкладка");
            tabControl.Items.Remove(tabControl.Items[tabControl.Items.Count - 1]);
            tabControl.Items.Add(tab);
            tabControl.Items.Add(CreatePlusTabItem());
        }

    }
}
