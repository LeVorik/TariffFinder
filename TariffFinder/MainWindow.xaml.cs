using System;
using System.Collections.Generic;
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

namespace TariffFinder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Entities en = new Entities();

        private List<OperatorItem> operatorItems = new List<OperatorItem>();
        private List<TariffItem> tariffItems = new List<TariffItem>();

        public MainWindow()
        {
            InitializeComponent();
            LoadOperators();
        }

        private void LoadOperators()
        {
            operatorItems = en.Operators.Select(o => new OperatorItem { Id = o.Id, Name = o.Name, IsSelected = false }).ToList();

            OperatorsListBox.ItemsSource = operatorItems;
        }

        private void ShowTariffsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOperatorIds = operatorItems
                .Where(o => o.IsSelected)
                .Select(o => o.Id)
                .ToList();

            if (!selectedOperatorIds.Any())
            {
                MessageBox.Show("Выберите хотя бы одного оператора");
                return;
            }

            tariffItems = en.Tariffs
                .Where(t => selectedOperatorIds.Contains(t.OperatorId))
                .Select(t => new TariffItem
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Minutes = t.Minutes,
                    DataGB = t.DataGB,
                    Sms = t.Sms,
                    IsSelected = false
                })
                .ToList();

            TariffsListBox.ItemsSource = tariffItems;
        }

        private void CompareButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTariffs = tariffItems.Where(t => t.IsSelected).ToList();

            if (selectedTariffs.Count != 2)
            {
                MessageBox.Show("Пожалуйста, выберите ровно 2 тарифа для сравнения.");
                return;
            }

            var compareWindow = new CompareWindow(selectedTariffs[0], selectedTariffs[1]);
            compareWindow.Owner = this;
            compareWindow.ShowDialog();
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class OperatorItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class TariffItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Minutes { get; set; }
        public int DataGB { get; set; }
        public int Sms { get; set; }
        public bool IsSelected { get; set; }
    }
}
