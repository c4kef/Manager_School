using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Manager.Views.User;
using Xamarin.Forms;

namespace Manager.ViewModels
{
    public class MainPageView : ViewModelBase
    {
        public string login;

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
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
            //MainState = LayoutState.Loading;
            ClearAuthData();
            await ((NavigationPage) Application.Current.MainPage).PushAsync (new Dashboard());
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