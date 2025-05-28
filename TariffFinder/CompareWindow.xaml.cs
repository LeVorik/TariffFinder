using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TariffFinder
{
    /// <summary>
    /// Логика взаимодействия для CompareWindow.xaml
    /// </summary>
    public partial class CompareWindow : Window
    {
        public TariffItem Tariff1 { get; set; }
        public TariffItem Tariff2 { get; set; }

        // Цвета для подсветки
        public Brush PriceHighlight1 => Tariff1.Price < Tariff2.Price ? Brushes.Green : Brushes.Black;
        public Brush PriceHighlight2 => Tariff2.Price < Tariff1.Price ? Brushes.Green : Brushes.Black;

        public Brush MinutesHighlight1 => Tariff1.Minutes > Tariff2.Minutes ? Brushes.Green : Brushes.Black;
        public Brush MinutesHighlight2 => Tariff2.Minutes > Tariff1.Minutes ? Brushes.Green : Brushes.Black;

        public Brush DataHighlight1 => Tariff1.DataGB > Tariff2.DataGB ? Brushes.Green : Brushes.Black;
        public Brush DataHighlight2 => Tariff2.DataGB > Tariff1.DataGB ? Brushes.Green : Brushes.Black;

        public Brush SmsHighlight1 => Tariff1.Sms > Tariff2.Sms ? Brushes.Green : Brushes.Black;
        public Brush SmsHighlight2 => Tariff2.Sms > Tariff1.Sms ? Brushes.Green : Brushes.Black;

        public CompareWindow(TariffItem tariff1, TariffItem tariff2)
        {
            Tariff1 = tariff1;
            Tariff2 = tariff2;
            InitializeComponent();
            DataContext = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
    public class BrushToLightBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                Color color = brush.Color;
                // Легкий цвет — увеличим яркость и снизим насыщенность
                Color lightColor = Color.FromArgb(40, color.R, color.G, color.B);
                return new SolidColorBrush(lightColor);
            }
            return new SolidColorBrush(Color.FromArgb(40, 0, 255, 0)); // Зеленоватый по умолчанию
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
