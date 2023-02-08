using BankProject.Models;
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

namespace BankProject
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        // открытие страницы регистрации
        private void RegBtn_MouseDown(object sender, MouseButtonEventArgs e) => MainWindow.SetFrame(new RegPage());

        private void AuthBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // поиск первого элемента из базы данных, таблицы Customers, по номеру и паролю
            Customer? findClient = MainWindow.Context.Customers.FirstOrDefault(c => c.PhoneNumber == PhoneTextBox.Text && c.Password == PasswordTextBox.Password);
            
            // если найден (не равен null)
            if (findClient != null)
            {
                // указываем текущего пользователя и его карту
                MainWindow.currentCustomer = findClient;
                MainWindow.currentCard = MainWindow.Context.BankCards.ToList()[findClient.Id - 1];

                // указываем найденного клиента в качестве контекста данных (биндинг)
                
                MainWindow.mainPage.DataContext = findClient;
                MainWindow.CardWindow.DataContext = MainWindow.currentCard;

                // переходим на главную страницу
                MainWindow.SetFrame(MainWindow.mainPage);
            }
            // если равен null
            else
            {
                MessageBox.Show("Пароль неверный или такого пользователя не существует.");
            }
        }
    }
}
