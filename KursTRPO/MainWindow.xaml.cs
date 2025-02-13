using KursTRPO.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KursTRPO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isAdmin;
        private Kurs777Context _context;
        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            _isAdmin = isAdmin;
            _context = new Kurs777Context();

            // Если пользователь не администратор, скрываем кнопки редактирования и удаления
            if (!_isAdmin)
            {
                AddButtonProducts.Visibility = Visibility.Collapsed;
                EditButtonProducts.Visibility = Visibility.Collapsed;
                DeleteButtonProducts.Visibility = Visibility.Collapsed;

                AddButtonSuppliers.Visibility = Visibility.Collapsed;
                EditButtonSuppliers.Visibility = Visibility.Collapsed;
                DeleteButtonSuppliers.Visibility = Visibility.Collapsed;

                AddButtonOrders.Visibility = Visibility.Collapsed;
                EditButtonOrders.Visibility = Visibility.Collapsed;
                DeleteButtonOrders.Visibility = Visibility.Collapsed;
            }
            LoadDBInDataGrid();
        }
        public void LoadDBInDataGrid()
        {
            using (Kurs777Context _context = new Kurs777Context())
            {
                ProductsDataGrid.ItemsSource = _context.Products.ToList();

                OrdersDataGrid.ItemsSource = _context.Orders.ToList();

                SuppliersDataGrid.ItemsSource = _context.Suppliers.ToList();
            }
        }

        private void AddButtonProducts_Click(object sender, RoutedEventArgs e)
        {
            var addProductsWindow = new AddProductsWindow
            {
                Title = "Добавить запись"
            };
            addProductsWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void EditButtonProducts_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                var addProductsWindow = new AddProductsWindow(selectedProduct)
                {
                    Title = "Редактировать запись"
                };
                addProductsWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DeleteButtonProducts_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Product row = (Product)ProductsDataGrid.SelectedItem;
                    if (row != null)
                    {
                        using (Kurs777Context _context = new Kurs777Context())
                        {
                            _context.Products.Remove(row);
                            _context.SaveChanges();

                            // Находим максимальный ID в таблице
                            var maxId = _context.Products.Max(p => (int?)p.ProductId) ?? 0;

                            // Сбрасываем автоинкремент
                            _context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Products', RESEED, {maxId})");
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                ProductsDataGrid.Focus();
            }
        }

        private void DeleteButtonSuppliers_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Supplier row = (Supplier)SuppliersDataGrid.SelectedItem;
                    if (row != null)
                    {
                        using (Kurs777Context _context = new Kurs777Context())
                        {
                            _context.Suppliers.Remove(row);
                            _context.SaveChanges();

                            // Находим максимальный ID в таблице
                            var maxId = _context.Suppliers.Max(s => (int?)s.SupplierId) ?? 0;

                            // Сбрасываем автоинкремент
                            _context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Suppliers', RESEED, {maxId})");
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                SuppliersDataGrid.Focus();
            }
        }

        private void EditButtonSuppliers_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem is Supplier selectedSupplier)
            {
                var addSuppliersWindow = new AddSuppliersWindow(selectedSupplier)
                {
                    Title = "Редактирование поставщика"
                };
                addSuppliersWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void AddButtonSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var addSuppliersWindow = new AddSuppliersWindow
            {
                Title = "Добавить поставщика"
            };
            addSuppliersWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void EditButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order selectedOrder)
            {
                var addOrdersWindow = new AddOrdersWindow(selectedOrder)
                {
                    Title = "Редактировать заказ"
                };
                addOrdersWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DeleteButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Order row = (Order)OrdersDataGrid.SelectedItem;
                    if (row != null)
                    {
                        using (Kurs777Context _context = new Kurs777Context())
                        {
                            _context.Orders.Remove(row);
                            _context.SaveChanges();

                            // Находим максимальный ID в таблице
                            var maxId = _context.Orders.Max(o => (int?)o.OrderId) ?? 0;

                            // Сбрасываем автоинкремент
                            _context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Orders', RESEED, {maxId})");
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                OrdersDataGrid.Focus();
            }
        }

        private void AddButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            var addOrdersWindow = new AddOrdersWindow
            {
                Title = "Добавить заказ"
            };
            addOrdersWindow.ShowDialog();
            LoadDBInDataGrid();
        }
    }
}