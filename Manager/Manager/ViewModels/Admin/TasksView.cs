using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Manager.Views.Admin;
using Xamarin.Forms;

namespace Manager.ViewModels.Admin
{
    public class TasksView : ViewModelBase
    {
        public TasksView()
        {
            Tasks = new ObservableCollection<TaskObject>();
            
            new Task(async () => await UpdateTasks()).Start();
            
            NavigateToDetailTaskPageCommand = new Command<TaskObject>(async (model) => await ExecuteNavigateToDetailTaskPageCommand(model));
        }
        
        public Command NavigateToDetailTaskPageCommand { get; }
        
        public ObservableCollection<TaskObject> Tasks { get; set; }
        
        private async Task UpdateTasks()
        {
            var getTasks =
                await (new SqlCommand(
                    $"SELECT Requests.id, equipmentwork.started, o.number, equipmentwork.id, equipment.id, suppliername, equipment.name, e_status.statusname, etype.typename, u.FIO, comments, datecreation, commentsadmin, request_status.name, dateclose FROM Requests JOIN Users u on Requests.id_users = u.Id JOIN Office O on u.id_office = O.id JOIN Equipmentwork equipmentwork on Requests.id_equipmentwork = equipmentwork.id JOIN Equipmentstatus e_status on equipmentwork.id_status = e_status.id JOIN Requestsstatus request_status on Requests.id_requestsstatus = request_status.id JOIN Equipment equipment on equipmentwork.id_equipment = equipment.id JOIN Equipmenttype etype on equipment.id_type = etype.id JOIN Suppliers supplier on equipment.id_supplier = supplier.id",
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
        
        private async Task ExecuteNavigateToDetailTaskPageCommand(TaskObject model)
        {
            await App.NavigationPage.PushAsync(new DetailTask(model));
        }
    }
}