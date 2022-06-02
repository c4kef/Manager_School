using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailDevice : ContentPage
    {
        public DetailDevice(Device device)
        {
            DeviceInfo = device;
            
            InitializeComponent();
            NavigateToReportPageCommand = new Command(ExecuteNavigateToReportPageCommand);

            BindingContext = this;
        }
        
        public Device DeviceInfo { get; set; }
        
        public Command NavigateToReportPageCommand { get; }
        
        private async void ExecuteNavigateToReportPageCommand()
        {
            await App.NavigationPage.PushAsync(new ReportPage(DeviceInfo));
        }
    }
}