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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // фрейм для перехода между страницами
        // статик - чтобы использовать без ссылки на MainWindow
        private static Frame? MainFrameStatic { get; set; }
        internal static StackPanel? CardWindow { get; set; }
        // создание экземпляра контекста базы данных
        // статик - чтобы использовать без ссылки на MainWindow
        internal static BankContext Context = new();

        // текущий клиент
        internal static Customer? currentCustomer { get; set; }
        internal static BankCard? currentCard { get; set; }

        // единственный экземпляр главной страницы
        internal static MainPage mainPage = new();
        public MainWindow()
        {
            InitializeComponent();

            // передаем объект xaml в статическую переменную
            MainWindow.MainFrameStatic = MainFrameWindow;
            MainWindow.CardWindow = mainPage.CardWindow;
        }

        // метод перехода между страницами, to - страница куда нужно перейти
        public static void SetFrame(object to) => MainFrameStatic.Navigate(to);
    }
}
