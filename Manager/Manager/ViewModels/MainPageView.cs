using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Manager.ViewModels
{
    public class MainPageView : ViewModelBase
    {
        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructors

        public MainPageView()
        {
            LoginCommand = new Command(LoginCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void LoginCommandHandler()
        {
            Debug.WriteLine($"Login is: {Login}");

            switch (Login)
            {
                case "root":
                    await ((NavigationPage) Application.Current.MainPage).PushAsync(new Manager.Views.Admin.Dashboard());
                    break;
                case "user":
                    await ((NavigationPage) Application.Current.MainPage).PushAsync(new Manager.Views.User.Dashboard());
                    break;
            }
            
            ClearAuthData();
        }

        #endregion

        #region Private Functionality

        private void ClearAuthData()
        {
            Login = Password = string.Empty;
        }

        #endregion
    }
}