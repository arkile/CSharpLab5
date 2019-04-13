using System.Windows.Controls;
using CSharp_Lab5.ViewModel;

namespace CSharp_Lab5.View
{
    /// <summary>
    /// Логика взаимодействия для UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl
    {
        public UserListView()
        {
            InitializeComponent();
            DataContext = new ProcessListViewModel();
        }
    }
}
