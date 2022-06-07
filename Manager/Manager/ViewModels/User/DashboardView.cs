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
            Tasks = new ObservableCollection<TaskObject>();
            Devices = new ObservableCollection<Device>();

            new Task(async () =>
            {
                while (true)
                {
                    await UpdateDevices();
                    await UpdateTasks();
                    await Task.Delay(1_500);
                }
            }).Start();
            
            NavigateToDetailDevicePageCommand = new Command<Device>(async (model) => await ExecuteNavigateToDetailDevicePageCommand(model));
            NavigateToDetailTaskPageCommand = new Command<TaskObject>(async (model) => await ExecuteNavigateToDetailTaskPageCommand(model));
        }

        public string UserName => Globals.CurrentUser.FullName;

        private async Task UpdateDevices()
        {
            var getDevices =
                await (new SqlCommand(
                    $"SELECT Equipmentwork.id, typename, country, name, suppliername, fio, requisites, address, statusname, arrival, started, cancellation, Equipment.id FROM Equipmentwork JOIN Equipmentstatus status ON Equipmentwork.id_status = status.id JOIN Equipment on Equipmentwork.id_equipment = Equipment.id JOIN Equipmenttype etype on id_type = etype.id JOIN Suppliers supp on id_supplier = supp.id WHERE id_users = '{Globals.CurrentUser.Id}' AND statusname != N'Списан'",
                    Globals.connection)).ExecuteReaderAsync();

            if (!getDevices.HasRows)
                goto end;

            var tmpDevices = new List<Device>();
            
            while (await getDevices.ReadAsync())
                tmpDevices.Add(
                    new Device()
                    {
                        AttachDate = getDevices.GetValue(10) is DBNull ? DateTime.Now : getDevices.GetDateTime(10),
                        Cabinet = Globals.CurrentUser.Number,
                        WId = getDevices.GetInt32(0),
                        EId = getDevices.GetInt32(12),
                        Manufacturer = getDevices.GetString(4),
                        Model = getDevices.GetString(3),
                        Status = getDevices.GetString(8),
                        Type = getDevices.GetString(1)
                    });
            
            Devices.Clear();
            foreach (var device in tmpDevices)
                Devices.Add(device);
            
            end:
            getDevices.Close();
        }
        
        private async Task UpdateTasks()
        {
            var getTasks =
                await (new SqlCommand(
                    $"SELECT Requests.id, equipmentwork.started, o.number, equipmentwork.id, equipment.id, suppliername, equipment.name, e_status.statusname, etype.typename, u.FIO, comments, datecreation, commentsadmin, request_status.name, dateclose FROM Requests JOIN Users u on Requests.id_users = u.Id JOIN Office O on u.id_office = O.id JOIN Equipmentwork equipmentwork on Requests.id_equipmentwork = equipmentwork.id JOIN Equipmentstatus e_status on equipmentwork.id_status = e_status.id JOIN Requestsstatus request_status on Requests.id_requestsstatus = request_status.id JOIN Equipment equipment on equipmentwork.id_equipment = equipment.id JOIN Equipmenttype etype on equipment.id_type = etype.id JOIN Suppliers supplier on equipment.id_supplier = supplier.id WHERE Requests.id_users = {Globals.CurrentUser.Id}",
                    Globals.connection)).ExecuteReaderAsync();

            if (!getTasks.HasRows)
                goto end;

            var tmpTasks = new List<TaskObject>();

            while (await getTasks.ReadAsync())
            {
                var device = new Device()
                {
                    AttachDate = getTasks.GetValue(1) is DBNull ? DateTime.Now : getTasks.GetDateTime(1),
                    Cabinet = getTasks.GetInt32(2),
                    WId = getTasks.GetInt32(3),
                    EId = getTasks.GetInt32(4),
                    Manufacturer = getTasks.GetString(5),
                    Model = getTasks.GetString(6),
                    Status = getTasks.GetString(7),
                    Type = getTasks.GetString(8),
                    UserName = getTasks.GetString(9)
                };

                tmpTasks.Add(
                    new TaskObject()
                    {
                        Id = getTasks.GetInt32(0),
                        Description = getTasks.GetString(10),
                        TimeFrom = getTasks.GetDateTime(11).Date,
                        Title = $"Устройство {getTasks.GetString(6)}",
                        Device = device,
                        Comment = getTasks.GetValue(12) is DBNull ? string.Empty : getTasks.GetString(12),
                        Status = getTasks.GetString(13),
                        TimeEnd =  getTasks.GetValue(14) is DBNull ? DateTime.Now : getTasks.GetDateTime(14)
                    });
            }
            
            Tasks.Clear();
            foreach (var task in tmpTasks)
                Tasks.Add(task);
            
            end:
            getTasks.Close();
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