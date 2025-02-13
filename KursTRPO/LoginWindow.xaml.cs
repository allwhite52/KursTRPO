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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Kurs777Context _context;
        public LoginWindow()
        {
            InitializeComponent();
            _context = new Kurs777Context();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Проверяем наличие пользователя в базе данных
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Pass == password);

            if (user != null)
            {
                // Если логин и пароль правильные, проверяем, является ли пользователь администратором
                bool isAdmin = username == "admin";

                // Открываем главное окно
                MainWindow mainWindow = new MainWindow(isAdmin);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Если авторизация не удалась, показываем ошибку
                ErrorTextBlock.Text = "Неверный логин или пароль";
            }
        }
    }
}
