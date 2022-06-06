using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        private string _status;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
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
            Status = string.Empty;

            var dt = new DataTable();

            var checkLogin = await (new SqlCommand(
                    $"SELECT Users.Id, Status, FIO, P.post, o.number, o.housing, o.stat FROM Users JOIN Office O on Users.id_office = O.id JOIN Post P on Users.Post = P.id WHERE Login = '{Login}' AND Password = '{Password}'", Globals.connection))
                .ExecuteReaderAsync();

            if (!checkLogin.HasRows)
            {
                Status = "Неверный логин или пароль";
                checkLogin.Close();
                return;
            }

            await checkLogin.ReadAsync();

            Globals.CurrentUser = new UserData()
                {
                    Id = checkLogin.GetInt32(0),
                    IsAdmin = checkLogin.GetValue(1).GetType() != typeof(DBNull),
                    FullName = checkLogin.GetString(2),
                    Post = checkLogin.GetString(3),
                    Number = checkLogin.GetInt32(4),
                    Housing = checkLogin.GetInt32(5),
                    Stat = checkLogin.GetBoolean(6)
                };
            
            checkLogin.Close();

            if (Globals.CurrentUser.IsAdmin)
                await ((NavigationPage) Application.Current.MainPage).PushAsync(new Manager.Views.Admin.Dashboard());
            else
                await ((NavigationPage) Application.Current.MainPage).PushAsync(new Manager.Views.User.Dashboard());

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