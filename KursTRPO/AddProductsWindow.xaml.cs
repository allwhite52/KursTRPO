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
    /// Логика взаимодействия для AddProductsWindow.xaml
    /// </summary>
    public partial class AddProductsWindow : Window
    {
        private Product _product;
        private Kurs777Context _context;
        public AddProductsWindow()
        {
            InitializeComponent();
            _context = new Kurs777Context();
            LoadSuppliers();
            //this.Title = "Добавить товар"; // Устанавливаем заголовок окна
        }
        public AddProductsWindow(Product product) : this()
        {
            _product = product;
            //this.Title = "Редактировать товар"; // Устанавливаем заголовок окна
            // Заполняем поля данными из выбранного товара
            NameProductTextBox.Text = _product.NameProduct;
            TypeProductTextBox.Text = _product.TypeProduct;
            PriceTextBox.Text = _product.Price.ToString();
            StockTextBox.Text = _product.Stock.ToString();
            SupplierComboBox.SelectedItem = _context.Suppliers.FirstOrDefault(s => s.SupplierId == _product.SupplierId);
        }

        private void LoadSuppliers()
        {
            SupplierComboBox.ItemsSource = _context.Suppliers.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameProductTextBox.Text) || string.IsNullOrEmpty(TypeProductTextBox.Text) || string.IsNullOrEmpty(PriceTextBox.Text) || string.IsNullOrEmpty(StockTextBox.Text) || SupplierComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Отрицательные значения недопустимы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(StockTextBox.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Отрицательные значения запрещены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (Kurs777Context _context = new Kurs777Context())
            {
                var selectedSupplier = (Supplier)SupplierComboBox.SelectedItem;

                if (_product == null)
                {
                    // Добавление нового товара
                    _context.Products.Add(new Product
                    {
                        NameProduct = NameProductTextBox.Text,
                        TypeProduct = TypeProductTextBox.Text,
                        Price = decimal.Parse(PriceTextBox.Text),
                        Stock = int.Parse(StockTextBox.Text),
                        SupplierId = selectedSupplier.SupplierId
                    });
                }
                else
                {
                    // Редактирование существующего товара
                    _product.NameProduct = NameProductTextBox.Text;
                    _product.TypeProduct = TypeProductTextBox.Text;
                    _product.Price = decimal.Parse(PriceTextBox.Text);
                    _product.Stock = int.Parse(StockTextBox.Text);
                    _product.SupplierId = selectedSupplier.SupplierId;
                    _context.Products.Update(_product);
                }
                _context.SaveChanges();
            }
            this.Close();
        }

        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _);
        }

        private void StockTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
