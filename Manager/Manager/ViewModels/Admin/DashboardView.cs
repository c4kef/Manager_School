using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Manager.Views.Admin;
using Xamarin.Forms;

namespace Manager.ViewModels.Admin
{
    public class DashboardView : ViewModelBase
    {
        public DashboardView()
        {
            NavigateToDetailDevicePageCommand = new Command(async () => await ExecuteNavigateToDetailDevicePageCommand());
        }
        
        public string UserName => Globals.CurrentUser.FullName;
        
        public Command NavigateToDetailDevicePageCommand { get; }
        
        private async Task ExecuteNavigateToDetailDevicePageCommand()
        {//(select ID_tournament from tournaments where name = N'{dialog.Tournaments}')
            await App.NavigationPage.PushAsync(new Tasks());
        }
    }
}