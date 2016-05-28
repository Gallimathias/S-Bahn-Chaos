using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void ChangeProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (value == null && field == null)
                return;

            if (value != null && field != null && value.Equals(field))
                return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }


    class MainView : ContentPage
    {
        private readonly DelegateCommand _execCommand;
        public MainView()
        {
            Vm = new ViewModel();
            var usernameInput = new Entry();
            usernameInput.SetBinding(Entry.TextProperty, nameof(ViewModel.Username));

            var passwordInput = new Entry();
            passwordInput.IsPassword = true;
            passwordInput.SetBinding(Entry.TextProperty, nameof(ViewModel.Password));

            _execCommand = new DelegateCommand(OnLogin, o => !string.IsNullOrEmpty(Vm.Password) && !string.IsNullOrWhiteSpace(Vm.Username) && !Vm.LoggingIn);

            Vm.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(ViewModel.Password) || e.PropertyName == nameof(ViewModel.Username))
                {
                    _execCommand.RaiseCanExecuteChanged();
                    Vm.ShowError = false;
                }
            };

            var loginButton = new Button();
            loginButton.Command = _execCommand;
            loginButton.Text = "Anmelden";

            BindingContext = Vm;

            var errorLabel = new Label { Text = "Ungültige Anmeldung, Passwort oder Handynummer / Email nicht gefunden", TextColor = Color.Red };
            errorLabel.SetBinding(IsVisibleProperty, nameof(Vm.ShowError));
            Title = "ng";
            Content = new StackLayout
            {
                Children = {
                    errorLabel,
                    new Label {Text = "Handynummer / Email"},
                    usernameInput,
                    new Label {Text = "Passwort"},
                    passwordInput,
                    loginButton
                }
            };
        }

        private async void OnLogin(object obj)
        {
            Vm.LoggingIn = true;
            Vm.ShowError = false;
            _execCommand.RaiseCanExecuteChanged();

            if (true)
            {
                //Application.Current.MainPage = new MainPage();
            }
            else
            {
                Vm.ShowError = true;
                Vm.LoggingIn = false;
                _execCommand.RaiseCanExecuteChanged();
            }
        }

        public ViewModel Vm { get; set; }

        public class ViewModel : BaseViewModel
        {
            private string _password;
            private bool _showError;
            private string _username;

            public string Password
            {
                get { return _password; }
                set { ChangeProperty(ref _password, value); }
            }

            public string Username
            {
                get { return _username; }
                set { ChangeProperty(ref _username, value); }
            }

            public bool LoggingIn { get; set; }

            public bool ShowError
            {
                get { return _showError; }
                set { ChangeProperty(ref _showError, value); }
            }
        }
    }
}
