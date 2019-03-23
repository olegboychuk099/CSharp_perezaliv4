using System.Windows;
using Csharp_laba4.View;
using Csharp_laba4.ViewModel;

namespace Csharp_laba4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PersonInputView _personInputView;
        private UsersView _usersView;

        public MainWindow()
        {
            InitializeComponent();
            ShowUsersView();
        }

        private void ShowInputView()
        {
            MainGrid.Children.Clear();
            if (_personInputView == null)
                _personInputView = new PersonInputView(ShowUsersView);
            MainGrid.Children.Add(_personInputView);
        }

        private void ShowUsersView()
        {
            MainGrid.Children.Clear();
            if (_usersView == null)
                _usersView = new UsersView(ShowInputView);
            else
                ((UsersViewModel)_usersView.DataContext).UpdateUsers();
            MainGrid.Children.Add(_usersView);
        }
    }
}
