using ApplicationForBD.ApplicationDataBases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace ApplicationForBD.Pages
{
    public partial class AdaptiveTablePage : Page
    {
        Frame frameAdmin = null;
        string table = null;
        string nameFirstColumn = null;
        public AdaptiveTablePage()
        {
            InitializeComponent();
        }
        public AdaptiveTablePage(Frame frameName, string tableName)
        {
            InitializeComponent();
            frameAdmin = frameName;
            table = tableName;
            ListInitialize();
        }
        private void ListInitialize()
        {
            GridView myGridView = new GridView();
            myGridView.AllowsColumnReorder = true;

            SqlDataReader reader = AppConnect.GetOpenReader($"SELECT * FROM [dbo].[{table}]");

            for (int i = 0; i < reader.FieldCount; i++)
            {
                nameFirstColumn = reader.GetName(0);
                myGridView.Columns.Add(GridViewColumnCreate(reader.GetName(i)));
            }
            
            listService.View = myGridView;

            AppConnect.CloseConnection();

            RefreshList(listService, $"SELECT * FROM [dbo].[{table}]", table);
        }
        private User ReturnListUser(IDataRecord record)
            => new User(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4));
        private AttachedProduct ReturnListAttached(IDataRecord record)
            => new AttachedProduct(Convert.ToInt32(record.GetValue(0)), Convert.ToInt32(record.GetValue(1)));
        private Client ReturnListClient(IDataRecord record)
            => new Client(record.GetInt32(0), record.GetString(1), record.GetString(2), 
                Convert.ToString(record.GetValue(3)), record.GetDateTime(4), record.GetDateTime(5), record.GetString(6), record.GetString(7),
                record.GetString(8), Convert.ToString(record.GetValue(9)));
        private ClientService ReturnListClientService(IDataRecord record)
            => new ClientService(Convert.ToInt32(record.GetValue(0)), Convert.ToInt32(record.GetValue(1)), 
                Convert.ToInt32(record.GetValue(2)), Convert.ToDateTime(record.GetValue(3)), 
                Convert.ToString(record.GetValue(4)));
        private DocumentByService ReturnListDocumentByService(IDataRecord record)
            => new DocumentByService(Convert.ToInt32(record.GetValue(0)), Convert.ToInt32(record.GetValue(1)), Convert.ToString(record.GetValue(2)));
        private Gender ReturnListGender(IDataRecord record)
            => new Gender(record.GetString(0), record.GetString(1));
        private Manufacturer ReturnListManufacturer(IDataRecord record)
            => new Manufacturer(record.GetInt32(0), record.GetString(1), record.GetDateTime(2));
        private Product ReturnListProduct(IDataRecord record)
            => new Product(record.GetInt32(0), record.GetString(1), 
                record.GetDecimal(2), record.GetString(3), record.GetString(4), record.GetBoolean(5), record.GetInt32(6));
        private ProductPhoto ReturnListProductPhoto(IDataRecord record)
            => new ProductPhoto(record.GetInt32(0), record.GetInt32(1), record.GetString(2));
        private ProductSale ReturnListProductSale(IDataRecord record)
            => new ProductSale(record.GetInt32(0), record.GetDateTime(1), record.GetInt32(2), record.GetInt32(3), record.GetInt32(4));
        private role ReturnListrole(IDataRecord record)
            => new role(record.GetInt32(0), record.GetString(1));
        private ServicePhoto ReturnListServicePhoto(IDataRecord record)
            => new ServicePhoto(record.GetInt32(0), record.GetInt32(1), record.GetString(2));
        private Tag ReturnListTag(IDataRecord record)
            => new Tag(record.GetInt32(0), record.GetString(1), record.GetString(2));
        private TagOfClient ReturnListTagOfClient(IDataRecord record)
            => new TagOfClient(record.GetInt32(0), record.GetInt32(1));
        private void RefreshList(ListView list, string query, string table)
        {
            ArrayList values = new ArrayList();
            List<ArrayList> valuesMagic = new List<ArrayList>();
            ArrayList temp = new ArrayList();
            SqlDataReader reader = AppConnect.GetOpenReader(query);

            switch (table.ToLower())
            {
                case "attachedproduct":
                    while(reader.Read())
                        values.Add(ReturnListAttached(reader));
                    break;
                case "client":
                    while(reader.Read())
                        values.Add(ReturnListClient(reader));
                    break;
                case "clientservice":
                    while (reader.Read())
                        values.Add(ReturnListClientService(reader));
                    break;
                case "documentbyservice":
                    while (reader.Read())
                        values.Add(ReturnListDocumentByService(reader));
                    break;
                case "gender":
                    while (reader.Read())
                        values.Add(ReturnListGender(reader));
                    break;
                case "manufacturer":
                    while (reader.Read())
                        values.Add(ReturnListManufacturer(reader));
                    break;
                case "product":
                    while (reader.Read())
                        values.Add(ReturnListProduct(reader));
                    break;
                case "productphoto":
                    while (reader.Read())
                        values.Add(ReturnListProductPhoto(reader));
                    break;
                case "productsale":
                    while (reader.Read())
                        values.Add(ReturnListProductSale(reader));
                    break;
                case "role":
                    while (reader.Read())
                        values.Add(ReturnListrole(reader));
                    break;
                case "servicephoto":
                    while (reader.Read())
                        values.Add(ReturnListServicePhoto(reader));
                    break;
                case "tag":
                    while (reader.Read())
                        values.Add(ReturnListTag(reader));
                    break;
                case "tagofclient":
                    while (reader.Read())
                        values.Add(ReturnListTagOfClient(reader));
                    break;
                case "user":
                    while(reader.Read())
                        values.Add(ReturnListUser(reader));
                    break;
                default:
                    while (reader.Read())
                    {
                        temp.Clear();
                        for (int i = 0; i < reader.FieldCount; i++)
                            temp.Add(reader.GetValue(i));
                        
                        valuesMagic.Add(temp);
                        list.ItemsSource = valuesMagic;
                    }
                    return;
            }
            
            
            list.ItemsSource = values;
        }
        private GridViewColumn GridViewColumnCreate(string name)
        {
            GridViewColumn gvc = new GridViewColumn();
            gvc.DisplayMemberBinding = new Binding(name);
            gvc.Header = name;
            gvc.Width = 100;
            return gvc;
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void orderDeskButton_Click(object sender, RoutedEventArgs e)
        {

        }
        dynamic obj;
        private void listService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editButton.Opacity = 1;
            deleteButton.Opacity = 1;

            editButton.IsEnabled = true;
            deleteButton.IsEnabled = true;

            obj = listService.SelectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frameAdmin.Navigate(new addDataPage(table, frameAdmin));
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Type t = obj.GetType();
            
            SqlCommand sql = new SqlCommand($"Delete [dbo].[{table}] WHERE {nameFirstColumn} = {t.GetProperty(nameFirstColumn).GetValue(obj)}", AppConnect.GetConnection);

            if (sql.ExecuteNonQuery() == 1)
                MessageBox.Show("Данные удалены!");
            else
                MessageBox.Show("Ошибка! Данные не удалены");
            
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
