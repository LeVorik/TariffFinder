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
    /// Логика взаимодействия для BestTariffsWindow.xaml
    /// </summary>
    public partial class BestTariffsWindow : Window
    {
        public string BestTitle { get; set; }
        public List<BestTariffViewModel> Tariffs { get; set; }

        public BestTariffsWindow(string title, IEnumerable<TariffItem> tariffs, Func<TariffItem, double> selector, bool isMin = false)
        {
            InitializeComponent();
            Console.WriteLine($"!!!!!!!!!!!!!!!!{title}");
            BestTitle = title;

            double bestValue = isMin ? tariffs.Min(selector) : tariffs.Max(selector);

            Tariffs = tariffs.Select(t => new BestTariffViewModel
            {
                Name = t.Name,
                DisplayValue = FormatValue(selector(t), title),
                IsBest = selector(t) == bestValue
            }).ToList();

            DataContext = this;
        }

        private string FormatValue(double value, string title)
        {
            if (title.Contains("цене")) return $"{value:F2} руб/мес";
            if (title.Contains("минутам")) return $"{value} мин";
            if (title.Contains("интернету")) return $"{value} ГБ";
            if (title.Contains("СМС")) return $"{value} СМС";
            return value.ToString();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }

    public class BestTariffViewModel
    {
        public string Name { get; set; }
        public string DisplayValue { get; set; }
        public bool IsBest { get; set; }
    }
}
