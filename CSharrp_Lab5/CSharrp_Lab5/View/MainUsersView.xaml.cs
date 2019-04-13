using System.Windows;
using CSharp_Lab5.ViewModel;

namespace CSharp_Lab5.View
{
    /// <summary>
    /// Логика взаимодействия для MainUsersView.xaml
    /// </summary>
    public partial class MainUsersView : Window
    {
        public MainUsersView()
        {

            InitializeComponent();
            DataContext = new MainViewModel();

        }


    }
}
