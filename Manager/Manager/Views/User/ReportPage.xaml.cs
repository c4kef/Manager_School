using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        }
        
        public string Comment { get; set; }
        
        public Device DeviceInfo { get; set; }
        
        private async void SendReport(object sender, EventArgs e)
        {
            await (new SqlCommand(
                $"INSERT INTO Requests (id_users, id_equipmentwork, id_requestsstatus, comments, datecreation, datechange) VALUES ((Select Users.Id from Users WHERE Users.Id = {Globals.CurrentUser.Id}), (Select Equipmentwork.id from Equipmentwork WHERE Equipmentwork.id = {DeviceInfo.WId}), (Select Requestsstatus.id from Requestsstatus WHERE Requestsstatus.id = 1), N'{Comment}', '{DateTime.Now}', '{DateTime.Now}')",
                Globals.connection)).ExecuteNonQueryAsync();
            await DisplayAlert("Отправлено", "Ваша заявка отправлена", "OK");
        }
    }
}