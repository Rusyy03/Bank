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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ExitBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.currentCustomer = null;
            MainWindow.currentCard = null;
            MainWindow.mainPage.DataContext = null;

            MainWindow.SetFrame(new AuthPage());
        }
        private void TransferBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // ищем по номеру
            Customer? findClient = MainWindow.Context.Customers.FirstOrDefault(c => c.PhoneNumber == PhoneTextBox.Text);

            // если найден (не равен null)
            if (findClient != null)
            {
                // выбераем числа из поля для ввода суммы перевода, и записываем их
                // в переменную amount
                decimal.TryParse(AmountTextBox.Text, out decimal amount);
                
                // если сумма перевода превышает баланс карты пользователя
                // -1 потому что в базе данных записи начинаются с 1, а в C# с 0
                if (amount > MainWindow.Context.BankCards.ToList()[MainWindow.currentCustomer.Id - 1].Balance)
                {
                    MessageBox.Show("Недостаточно средств.");
                }
                else if (amount < 10)
                {
                    MessageBox.Show("Минимальная сумма перевода - 10.");
                }
                else
                {
                    // переводим деньги на карту найденного пользователя
                    MainWindow.Context.BankCards.ToList()[findClient.Id - 1].Balance += amount;

                    // отнимаем деньги у текущего пользователя
                    MainWindow.currentCard.Balance -= amount;

                    // обновляем текст на главной странице
                    MainWindow.mainPage.CurrentBalance.Content = MainWindow.currentCard.Balance.ToString();

                    //сохраняем изменения в базе данных
                    MainWindow.Context.SaveChanges();

                    MessageBox.Show("Перевод отправлен.");
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void TransferBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CardWindow.Visibility = Visibility.Collapsed;
            SetPasswordWindow.Visibility = Visibility.Collapsed;

            TransferWindow.Visibility = Visibility.Visible;
        }

        private void CardBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CardWindow.Visibility = Visibility.Visible;

            TransferWindow.Visibility = Visibility.Collapsed;
            SetPasswordWindow.Visibility = Visibility.Collapsed;
        }

        private void SetPasswordBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // если подтверждение старого пароля верно
            if (OldPasswordTextBox.Text == MainWindow.currentCustomer.Password)
            {
                // изменяем пароль
                MainWindow.currentCustomer.Password = NewPasswordTextBox.Text;

                MessageBox.Show("Пароль изменен");

                // сохраняем базу данных
                MainWindow.Context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Текущий пароль не совпадает.");
            }
        }

        private void SetPasswordBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPasswordWindow.Visibility = Visibility.Visible;

            CardWindow.Visibility = Visibility.Collapsed;
            TransferWindow.Visibility = Visibility.Collapsed;
        }
    }
}
