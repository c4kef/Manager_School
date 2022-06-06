using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Manager.Views.User;
using Xamarin.Forms;

namespace Manager.ViewModels.User
{
    public class DashboardView : ViewModelBase
    {
        public DashboardView()
        {
            Tasks = new ObservableCollection<TaskObject>(new List<TaskObject>()
            {
                new TaskObject()
                {
                    Description = "Тут что-то типа описания, пиши сколько хочешь и разницы не будет ;)",
                    TimeFrom = DateTime.Now,
                    Title = "Тут заголовок страницы"
                },
                new TaskObject()
                {
                    Description = "Тут что-то типа описания, пиши сколько хочешь и разницы не будет ;)",
                    TimeFrom = DateTime.Now,
                    Title = "Тут заголовок страницы"
                },
                new TaskObject()
                {
                    Description = "Тут что-то типа описания, пиши сколько хочешь и разницы не будет ;)",
                    TimeFrom = DateTime.Now,
                    Title = "Тут заголовок страницы"
                },
                new TaskObject()
                {
                    Description = "Тут что-то типа описания, пиши сколько хочешь и разницы не будет ;)",
                    TimeFrom = DateTime.Now,
                    Title = "Тут заголовок страницы"
                },
                new TaskObject()
                {
                    Description = "Тут что-то типа описания, пиши сколько хочешь и разницы не будет ;)",
                    TimeFrom = DateTime.Now,
                    Title = "Тут заголовок страницы"
                }
            });

            Devices = new ObservableCollection<Device>();

            new Task(async () => await UpdateDevices()).Start();
            
            NavigateToDetailDevicePageCommand = new Command<Device>(async (model) => await ExecuteNavigateToDetailDevicePageCommand(model));
            NavigateToDetailTaskPageCommand = new Command<TaskObject>(async (model) => await ExecuteNavigateToDetailTaskPageCommand(model));
        }

        private async Task UpdateDevices()
        {
            var getDevices =
                await (new SqlCommand(
                    $"SELECT Equipmentwork.id, typename, country, name, suppliername, fio, requisites, address, statusname, arrival, started, cancellation FROM Equipmentwork JOIN Equipmentstatus status ON Equipmentwork.id_status = status.id JOIN Equipment on Equipmentwork.id_equipment = Equipment.id JOIN Equipmenttype etype on id_type = etype.id JOIN Suppliers supp on id_supplier = supp.id WHERE id_users = '{Globals.CurrentUser.Id}'",
                    Globals.connection)).ExecuteReaderAsync();

            if (!getDevices.HasRows)
                goto end;

            Devices.Clear();

             while (await getDevices.ReadAsync())
                 Devices.Add(
                     new Device()
                     {
                        AttachDate = getDevices.GetValue(10) is DBNull ? DateTime.Now : getDevices.GetDateTime(10),
                        Cabinet = Globals.CurrentUser.Number,
                        Id = getDevices.GetInt32(0),
                        Manufacturer = getDevices.GetString(4),
                        Model = getDevices.GetString(3),
                        Status = getDevices.GetString(8),
                        Type = getDevices.GetString(1)
                    });
            
            end:
            getDevices.Close();
        }

        public ObservableCollection<TaskObject> Tasks { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public Command NavigateToDetailDevicePageCommand { get; }
        public Command NavigateToDetailTaskPageCommand { get; }
        
        private async Task ExecuteNavigateToDetailDevicePageCommand(Device model)
        {
            await App.NavigationPage.PushAsync(new DetailDevice(model));
        }
        
        private async Task ExecuteNavigateToDetailTaskPageCommand(TaskObject model)
        {
            await App.NavigationPage.PushAsync(new DetailTask(model));
        }
    }
}