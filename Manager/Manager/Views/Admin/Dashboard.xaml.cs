using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.ViewModels.Admin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace Manager.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new DashboardView();
        }

        private async void ScanResult(Result result)
        {
            var getDevices =
                await (new SqlCommand(
                    $"SELECT Equipmentwork.id, typename, country, name, suppliername, supp.fio, requisites, address, statusname, arrival, started, users.FIO, cancellation, Equipment.id FROM Equipmentwork JOIN Users users ON Equipmentwork.id_users = users.Id JOIN Equipmentstatus status ON Equipmentwork.id_status = status.id JOIN Equipment on Equipmentwork.id_equipment = Equipment.id JOIN Equipmenttype etype on id_type = etype.id JOIN Suppliers supp on id_supplier = supp.id WHERE qr = '{result.Text}'",
                    Globals.connection)).ExecuteReaderAsync();

            if (!getDevices.HasRows)
                goto end;

            await getDevices.ReadAsync();
            await App.NavigationPage.PushAsync(new DetailDevice(new Device()
            {
                AttachDate = getDevices.GetValue(10) is DBNull ? DateTime.Now : getDevices.GetDateTime(10),
                Cabinet = Globals.CurrentUser.Number,
                WId = getDevices.GetInt32(0),
                EId = getDevices.GetInt32(13),
                Manufacturer = getDevices.GetString(4),
                Model = getDevices.GetString(3),
                Status = getDevices.GetString(8),
                Type = getDevices.GetString(1),
                UserName = getDevices.GetString(11)
            }));

            end:
            getDevices.Close();
        }
    }
}