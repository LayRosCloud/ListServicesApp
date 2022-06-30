using ApplicationForBD.ApplicationDataBases;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для addDataPage.xaml
    /// </summary>
    public partial class addDataPage : Page
    {
        private string table;
        private string queryString;
        private Frame frameAdmin;
        byte startOutPut = 0;
        List<string> temp = new List<string>();
        public addDataPage()
        {
            InitializeComponent();
        }
        public addDataPage(string tableData, Frame frame)
        {
            InitializeComponent();
            table = tableData;
            frameAdmin = frame;
            backButton.Content = "<";
            queryString = $"SELECT * FROM [dbo].[{table}]";

            checkedId.IsChecked = HasIdetinty();
            
        }
        private bool HasIdetinty()
        {
            string selectIndentity = $@"select COLUMN_NAME, TABLE_NAME
                        from INFORMATION_SCHEMA.COLUMNS
                        where COLUMNPROPERTY(object_id(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 AND TABLE_NAME = '{table}'
                        order by TABLE_NAME ";
            SqlCommand sqlCommand1 = new SqlCommand(selectIndentity, AppConnect.GetConnection);
            AppConnect.OpenConnection();
            SqlDataReader reader1 = sqlCommand1.ExecuteReader();
            bool hasRows = reader1.HasRows;
            
            AppConnect.CloseConnection();
            return hasRows;
        }
        private void InitializeTextBoxs()
        {
            temp.Clear();
            stackPanelLeft.Children.Clear();
            stackPanelRight.Children.Clear();
            SqlDataReader reader = AppConnect.GetOpenReader(queryString);

            for (int i = startOutPut; i < reader.FieldCount; i++)
            {
                AddTextBlock(reader.GetName(i));
                AddTextBox(reader.GetDataTypeName(i));
                temp.Add(reader.GetDataTypeName(i));                
            }

            AppConnect.CloseConnection();
        }

        private void AddTextBlock(string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = Brushes.White;
            textBlock.FontSize = 30;
            textBlock.Margin = new Thickness(0, 0, 0, 5);
            stackPanelLeft.Children.Add(textBlock);
        }
        private void AddTextBox(string type)
        {
            TextBox textBox = new TextBox();
            textBox.Foreground = Brushes.White;
            textBox.Background = Brushes.Transparent;
            textBox.FontSize = 30;
            textBox.Margin = new Thickness(0, 0, 0, 4);
            textBox.BorderThickness = new Thickness(0, 0, 0, 1);
            textBox.BorderBrush = Brushes.White;
            textBox.Width = 300;
            switch (type.ToLower())
            {
                case "int":
                    textBox.TextChanged += TextBoxInt_TextChanged;
                    break;
                case "nvarchar":
                    textBox.TextChanged += TextBoxNVarChar_TextChanged;
                    break;
                case "money":
                    textBox.TextChanged += TextBoxMoney_TextChanged;
                    break;
                case "datetime":
                    DatePicker date1 = new DatePicker();
                    date1.BorderThickness = new Thickness(0, 0, 0, 1);
                    date1.Width = 300;
                    date1.Margin = new Thickness(0, 0, 0, 4);
                    date1.FontSize = 30;
                    date1.Text = "Выберите дату...";
                    date1.Background = Brushes.Transparent;

                    stackPanelRight.Children.Add(date1);
                    return;
                case "float":
                    textBox.TextChanged += TextBoxFloat_TextChanged;
                    break;
                case "nchar":
                    textBox.TextChanged += TextBoxNChar_TextChanged;
                    break;
                case "date":
                    DatePicker date = new DatePicker();
                    date.BorderThickness = new Thickness(0);
                    date.Width = 300;
                    date.FontSize = 30;
                    date.BorderThickness = new Thickness(0, 0, 0, 1);
                    date.Background = Brushes.Transparent;
                    date.Margin = new Thickness(0, 0, 0, 4);
                    date.Text = "Выберите дату...";
                    date.SelectedDateChanged += Date_SelectedDateChanged;
                    stackPanelRight.Children.Add(date);
                    return;
            }
            stackPanelRight.Children.Add(textBox);
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TextBoxInt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            foreach (char letter in textBox.Text.ToCharArray())
            {
                if (!(letter >= '0' && letter <= '9'))
                {
                    ErrorMessage("Ошибка! Строка должна принимать только целые значения!", textBox, Brushes.Red, new Thickness(3));
                    return;
                }
                else
                    ErrorMessage("Введите целое значение", textBox, Brushes.White, new Thickness(0, 0, 0, 1));
                
            }
        }
        private void TextBoxNVarChar_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void TextBoxMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            foreach (char letter in textBox.Text.ToCharArray())
            {
                if (!(letter >= '0' && letter <= '9' || letter == '.' || letter ==','))
                {
                    ErrorMessage("Ошибка! Строка должна принимать только числовые значения!", textBox, Brushes.Red, new Thickness(3));
                    return;
                }
                if((letter >= '0' && letter <= '9') || letter == '.')
                    ErrorMessage("Введите числовое значение", textBox, Brushes.White, new Thickness(0, 0, 0, 1));
                
            }
        }
        private void ErrorMessage(string message, TextBox textBox, Brush brushes, Thickness thickness)
        {
            textBox.BorderThickness = thickness;
            textBox.BorderBrush = brushes;
            textBox.ToolTip = message;
        }

        private void TextBoxNChar_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (text.Text.Length != 1)
                ErrorMessage("Ошибка! введите 1 символ.", text, Brushes.Red, new Thickness(1));
            else
                ErrorMessage("Введите символ", text, Brushes.White, new Thickness(0, 0, 0, 1));
            
        }
        private void TextBoxFloat_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            foreach (char letter in textBox.Text.ToCharArray())
            {
                if (!(letter >= '0' && letter <= '9' || letter == '.'))
                {
                    ErrorMessage("Ошибка! Строка должна принимать только числовые значения!", textBox, Brushes.Red, new Thickness(3));
                    return;
                }
                else if (letter == ',')
                {
                    ErrorMessage("Ошибка! Поменяйте знак ',' на '.'!", textBox, Brushes.Red, new Thickness(3));
                    return;
                }
                else
                {
                    ErrorMessage("Введите числовое значение", textBox, Brushes.White, new Thickness(0, 0, 0, 1));
                }
            }
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            switch (table)
            {
                case "Service":
                    frameAdmin.Navigate(new choiseService(table, frameAdmin));
                    break;
                default:
                    frameAdmin.Navigate(new AdaptiveTablePage(frameAdmin, table));
                    break;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string query = $"INSERT INTO {table} Values(";
            TextBox text = null;
            DatePicker date = null;
            for (int i = 0; i < stackPanelRight.Children.Count; i++)
            {
                if (stackPanelRight.Children[i] is TextBox && i != stackPanelRight.Children.Count - 1)
                {
                    text = stackPanelRight.Children[i] as TextBox;
                    if (temp[i] == "int" || temp[i] == "double" || temp[i] == "float" || temp[i] == "money")   
                        query += text.Text.Replace(',', '.') + ", ";
                    else
                        query += "'"+text.Text+"'" + ", ";
                    
                }
                else if (stackPanelRight.Children[i] is DatePicker && i != stackPanelRight.Children.Count - 1)
                {
                    date = stackPanelRight.Children[i] as DatePicker;
                    query += date.Text + ", ";
                }
                else if (stackPanelRight.Children[i] is TextBox && i == stackPanelRight.Children.Count - 1)
                {
                    text = stackPanelRight.Children[i] as TextBox;
                    if (temp[i] == "int" || temp[i] == "double" || temp[i] == "float" || temp[i] == "money")
                        query += text.Text + ");";
                    else
                        query += "'" + text.Text + "'" + ");";
                }
                else if (stackPanelRight.Children[i] is DatePicker && i == stackPanelRight.Children.Count - 1)
                {
                    date = stackPanelRight.Children[i] as DatePicker;
                    query += date.Text + ")";
                }
            }
            SqlCommand sql = AppConnect.GetOpenSqlCommand(query);
            if (sql.ExecuteNonQuery() == 1)
                MessageBox.Show("Успех! Данные добавлены");
            
            else
                MessageBox.Show("Данные не записались=(");
            
            AppConnect.CloseConnection();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            startOutPut = 1;
            InitializeTextBoxs();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            startOutPut = 0;
            InitializeTextBoxs();
        }
    }
}
