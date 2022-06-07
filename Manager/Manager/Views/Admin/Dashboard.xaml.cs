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

        private static string GetCurrentPage()
        {
            var page = App.NavigationPage.Navigation.NavigationStack.Last();
            return page.Title;
        }
        
        private async void ScanResult(Result result)
        {
            if (GetCurrentPage() != "Dashboard")
                return;
            
            var getDevices =
                await (new SqlCommand(
                    $"SELECT Equipmentwork.id, typename, country, name, suppliername, supp.fio, requisites, address, statusname, arrival, started, users.FIO, cancellation, Equipment.id, O.number FROM Equipmentwork JOIN Users users ON Equipmentwork.id_users = users.Id JOIN Equipmentstatus status ON Equipmentwork.id_status = status.id JOIN Equipment on Equipmentwork.id_equipment = Equipment.id JOIN Equipmenttype etype on id_type = etype.id JOIN Suppliers supp on id_supplier = supp.id JOIN Office O on users.id_office = O.id WHERE qr = '{result.Text}'",
                    Globals.connection)).ExecuteReaderAsync();

            if (!getDevices.HasRows)
            {
                getDevices.Close();
                return;
            }

            await getDevices.ReadAsync();
            
            Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await App.NavigationPage.PushAsync(new DetailDevice(new Device()
                {
                    AttachDate = getDevices.GetValue(10) is DBNull ? DateTime.Now : getDevices.GetDateTime(10),
                    Cabinet = getDevices.GetInt32(14),
                    WId = getDevices.GetSqlInt32(0).Value,
                    EId = getDevices.GetInt32(13),
                    Manufacturer = getDevices.GetString(4),
                    Model = getDevices.GetString(3),
                    Status = getDevices.GetString(8),
                    Type = getDevices.GetString(1),
                    UserName = getDevices.GetString(11)
                }));
                
                getDevices.Close();
            });
        }
    }
}