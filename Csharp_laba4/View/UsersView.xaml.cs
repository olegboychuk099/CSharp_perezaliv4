using System;
using System.Windows.Controls;
using Csharp_laba4.ViewModel;

namespace Csharp_laba4.View
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView(Action showInputViewAction)
        {
            InitializeComponent();
            DataContext = new UsersViewModel(UsersDataGrid, showInputViewAction);
        }
    }
}
