using System;
using System.Windows.Controls;
using Csharp_laba4.ViewModel;

namespace Csharp_laba4.View
{
    /// <summary>
    /// Interaction logic for PersonInputView.xaml
    /// </summary>
    internal partial class PersonInputView : UserControl
    {
        public PersonInputView(Action usersViewAction)
        {
            InitializeComponent();
            DataContext = new PersonInputViewModel(usersViewAction);
        }
    }
}
