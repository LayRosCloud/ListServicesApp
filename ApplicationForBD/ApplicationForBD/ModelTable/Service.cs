using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class Service
    {
        private int id;
        private string title;
        private decimal cost;
        private double durationInSeconds;
        private string description;
        private string buttonText;
        private double discount;
        private string mainImagePath;
        private decimal discountPrice;
        private Visibility equalsDiscountAndCost;

        public int Id { get => id; set => id = value; }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public double DurationInSeconds
        {
            get { return durationInSeconds; }
            set { durationInSeconds = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        public string MainImagePath
        {
            get { return mainImagePath; }
            set { mainImagePath = value; }
        }
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = $"Изменить {id} запись"; }
        }
        public decimal DiscountPrice
        {
            get => discountPrice;
            set { discountPrice = Cost - (Cost * (Convert.ToDecimal(Discount) / 100)); }
        }
        public Visibility EqualsDiscountAndCost
        {
            get => equalsDiscountAndCost;
            set { if (Cost == DiscountPrice)
                    equalsDiscountAndCost = Visibility.Collapsed;
            else equalsDiscountAndCost = Visibility.Visible;
            }
        }

        public bool IsNull
        {
            get => Title == null;
        }

        public Service(int id ,string title, decimal cost, double duration, string description, double discount, string path)
        {
            Id = id;
            Title = title;
            Cost = cost;
            DurationInSeconds = duration;
            Description = description;
            Discount = discount;
            MainImagePath = path;
            DiscountPrice = 2;
            EqualsDiscountAndCost = Visibility.Collapsed;
            ButtonText = "";

        }
    }
}
