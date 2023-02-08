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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SetFrame(new AuthPage());
        }

        private void RegBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Random r = new();

            if (NameTextBox.Text != "" && NumberTextBox.Text != "" && PasswordTextBox.Text == ConfirmPasswordTextBox.Text)
            {
                Customer newCustomer = new()
                {
                    FullName = NameTextBox.Text,
                    PhoneNumber = NumberTextBox.Text,
                    Password = PasswordTextBox.Text,
                };
                BankCard card = new()
                {
                    HolderNavigation = newCustomer,
                    Balance = 0,
                    Number = $"4444 1234 4321 000{r.Next(9)}"
                };

                MainWindow.Context.Customers.Add(newCustomer);
                MainWindow.Context.BankCards.Add(card);

                MainWindow.Context.SaveChanges();

                MainWindow.currentCustomer = newCustomer;
                MainWindow.currentCard = card;

                MainWindow.mainPage.DataContext = newCustomer;
                MainWindow.CardWindow.DataContext = MainWindow.currentCard;

                MainWindow.SetFrame(new MainPage());
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных");
            }
        }
    }
}
