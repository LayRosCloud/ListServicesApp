
using System.Windows;
using System.Windows.Controls;
using ApplicationForBD.Pages;
using ApplicationForBD;
using System;

namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для RangeSlider.xaml
    /// </summary>
    public partial class RangeSlider : UserControl
    {
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(1d));


        public RangeSlider()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler FirstSlider;
        public event RoutedEventHandler SecondSlider;

        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(FirstSlider != null) FirstSlider.Invoke(sender, e);
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SecondSlider != null) SecondSlider.Invoke(sender, e);
        }



        private void UpperTextBox_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Convert.ToDouble(UpperTextBox.Text) <= UpperSlider.Minimum)
                UpperTextBox.Text = UpperSlider.Minimum.ToString();
            if (Convert.ToDouble(UpperTextBox.Text) >= UpperSlider.Maximum)
                UpperTextBox.Text = UpperSlider.Maximum.ToString();
            UpperSlider.Value = Convert.ToDouble(UpperTextBox.Text);
        }

        private void lowerTextBox_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Convert.ToDouble(lowerTextBox.Text) <= LowerSlider.Minimum)
                lowerTextBox.Text = LowerSlider.Minimum.ToString();
            if (Convert.ToDouble(lowerTextBox.Text) >= LowerSlider.Maximum)
                lowerTextBox.Text = LowerSlider.Maximum.ToString();
            LowerSlider.Value = Convert.ToDouble(lowerTextBox.Text);
        }

    }
}
