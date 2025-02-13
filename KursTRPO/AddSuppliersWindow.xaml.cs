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
    /// Логика взаимодействия для AddSuppliersWindow.xaml
    /// </summary>
    public partial class AddSuppliersWindow : Window
    {
        private Supplier _supplier; // Текущий поставщик (если редактирование)
        private Kurs777Context _context;
        public AddSuppliersWindow()
        {
            InitializeComponent();
            _context = new Kurs777Context();
        }
        public AddSuppliersWindow(Supplier supplier) : this()
        {
            _supplier = supplier;
            this.Title = "Редактировать поставщика"; // Устанавливаем заголовок окна
            // Заполняем поля данными из выбранного поставщика
            NameSupplierTextBox.Text = _supplier.NameSupplier;
            PhoneSupplierTextBox.Text = _supplier.PhoneNumber;
            AddressSupplierTextBox.Text = _supplier.SupplierAdress;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameSupplierTextBox.Text) || string.IsNullOrEmpty(PhoneSupplierTextBox.Text) || string.IsNullOrEmpty(AddressSupplierTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (Kurs777Context _context = new Kurs777Context())
            {
                if (_supplier == null)
                {
                    // Добавление нового поставщика
                    _context.Suppliers.Add(new Supplier
                    {
                        NameSupplier = NameSupplierTextBox.Text,
                        PhoneNumber = PhoneSupplierTextBox.Text,
                        SupplierAdress = AddressSupplierTextBox.Text
                    });
                }
                else
                {
                    // Редактирование существующего поставщика
                    _supplier.NameSupplier = NameSupplierTextBox.Text;
                    _supplier.PhoneNumber = PhoneSupplierTextBox.Text;
                    _supplier.SupplierAdress = AddressSupplierTextBox.Text;
                    _context.Suppliers.Update(_supplier);
                }
                _context.SaveChanges();
            }
            this.Close();
        }

        private void PhoneSupplierTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
