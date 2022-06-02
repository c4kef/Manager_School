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
    public partial class ReportPage : ContentPage
    {
        public ReportPage(Device device)
        {
            DeviceInfo = device;
            
            InitializeComponent();
            BindingContext = this;
            
            SendReport = new Command(async () => await ExecuteSendReport());
        }
        
        public Device DeviceInfo { get; set; }
        
        public Command SendReport { get; }
        
        private async Task ExecuteSendReport()
        {
            await DisplayAlert("Отправлено", "Ваша заявка отправлена", "OK");
        }
    }
}