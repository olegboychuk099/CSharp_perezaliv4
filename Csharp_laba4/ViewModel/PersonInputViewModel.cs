using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Csharp_laba4.Tools;
using Csharp_laba4.Tools.Exceptions;

namespace Csharp_laba4.ViewModel
{
    internal class PersonInputViewModel : INotifyPropertyChanged, ILoaderOwner
    {
        private string _name = "";
        private string _surname = "";
        private string _email = "";
        private DateTime _date = DateTime.Today;
        private bool _canExecute;
        private Visibility _loaderVisibility = Visibility.Hidden;
        private RelayCommand<object> _calculateAgeCommand;

        private RelayCommand<object> _returnToViewCommand;

        private readonly Action _closeAction;

        private bool _isControlEnabled = true;
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        internal PersonInputViewModel(Action closeAction)
        {
            LoaderManager.Instance.Initialize(this);
            _closeAction = closeAction;
            CanExecute = false;
        }

        #region Properties
        public bool CanExecute
        {
            get { return _canExecute; }
            private set
            {
                _canExecute = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        #endregion

        public RelayCommand<object> CalculateAgeCommand
        {
            get { return _calculateAgeCommand ?? (_calculateAgeCommand = new RelayCommand<object>(AgeCalcImpl)); }
        }

        public RelayCommand<object> ReturnToViewCommand
        {
            get { return _returnToViewCommand ?? (_returnToViewCommand = new RelayCommand<object>(RetToViewImpl)); }
        }
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        private async void AgeCalcImpl(object o)
        {
            LoaderManager.Instance.ShowLoader();
            CanExecute = false;
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    try
                    {
                        PersonControler.GetPerson = UsersCreator.CreateUser(_name, _surname, _email, _date);
                    }
                    catch (EmailException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    catch (PastBirthdayException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    catch (FutureBirthdayException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    catch (NameException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
                LoaderManager.Instance.HideLoader();
                _closeAction.Invoke();
                CanExecute = true;
                ClearInputValues();
        }

        private async void RetToViewImpl(object o)
        {
            await Task.Run(() =>
            {
                ClearInputValues();
                CanExecute = false;
            });
            _closeAction.Invoke();
        }

        private void ClearInputValues()
        {
            Date = DateTime.Today;
            Name = "";
            Surname = "";
            Email = "";
        }

        private bool CheckIfFilled()
        {
            return _name != "" && _surname != "" && _email != "";
        }

        #region Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}