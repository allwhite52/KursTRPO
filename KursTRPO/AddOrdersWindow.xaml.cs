using KursTRPO.Models;
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
using System.Windows.Shapes;

namespace KursTRPO
{
    /// <summary>
    /// Логика взаимодействия для AddOrdersWindow.xaml
    /// </summary>
    public partial class AddOrdersWindow : Window
    {
        private Order _order; // Текущий заказ (если редактирование)
        private Kurs777Context _context;

        public AddOrdersWindow()
        {
            InitializeComponent();
            _context = new Kurs777Context();
            LoadProducts();
            this.Title = "Добавить заказ"; // Устанавливаем заголовок окна
        }
        public AddOrdersWindow(Order order) : this()
        {
            _order = order;
            this.Title = "Редактировать заказ"; // Устанавливаем заголовок окна

            // Преобразуем DateOnly в DateTime
            OrderDatePicker.SelectedDate = _order.OrderDate.ToDateTime(TimeOnly.MinValue);

            // Заполняем остальные поля
            ProductOrderComboBox.SelectedItem = _context.Products.FirstOrDefault(p => p.ProductId == _order.ProductId);
            QuantityOrderTextBox.Text = _order.Quantyti.ToString();
            PriceOrderTextBox.Text = _order.PriceOrder.ToString();
        }
        private void LoadProducts()
        {
            ProductOrderComboBox.ItemsSource = _context.Products.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDatePicker.SelectedDate == null || ProductOrderComboBox.SelectedItem == null || string.IsNullOrEmpty(QuantityOrderTextBox.Text) || string.IsNullOrEmpty(PriceOrderTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(QuantityOrderTextBox.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Отрицательные значения недопустимы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!decimal.TryParse(PriceOrderTextBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Отрицательные значения недопустимы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (Kurs777Context _context = new Kurs777Context())
            {
                var selectedProduct = (Product)ProductOrderComboBox.SelectedItem;

                // Преобразуем DateTime в DateOnly
                DateOnly orderDate = DateOnly.FromDateTime(OrderDatePicker.SelectedDate.Value);

                if (_order == null)
                {
                    // Добавление нового заказа
                    _context.Orders.Add(new Order
                    {
                        OrderDate = orderDate, // Используем DateOnly
                        ProductId = selectedProduct.ProductId,
                        Quantyti = int.Parse(QuantityOrderTextBox.Text),
                        PriceOrder = decimal.Parse(PriceOrderTextBox.Text),
                        TotalPrice = int.Parse(QuantityOrderTextBox.Text) * decimal.Parse(PriceOrderTextBox.Text)
                    });
                }
                else
                {
                    // Редактирование существующего заказа
                    _order.OrderDate = orderDate; // Используем DateOnly
                    _order.ProductId = selectedProduct.ProductId;
                    _order.Quantyti = int.Parse(QuantityOrderTextBox.Text);
                    _order.PriceOrder = decimal.Parse(PriceOrderTextBox.Text);
                    _order.TotalPrice = int.Parse(QuantityOrderTextBox.Text) * decimal.Parse(PriceOrderTextBox.Text);
                    _context.Orders.Update(_order);
                }
                _context.SaveChanges();
            }
            this.Close();
        }

        // Проверка ввода только чисел для количества
        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        // Проверка ввода только чисел для цены
        private void PriceOrderTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _);
        }
    }
}
