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
            SelectedStatusIndex = 0;
        }
        
        public List<string> StatusList => new List<string> {"Без фильтра", "На рассмотрений", "В работе", "Отклонён", "Завершён"};

        public int SelectedStatusIndex { get; private set; }
        
        public string SelectedStatus
        {
            set
            {
                var id = StatusList.FindIndex(status => status == value);
                
                if (id == -1)
                    return;

                SelectedStatusIndex = id;
                
                new Task(async () => await UpdateTasks(id)).Start();
            }
        }

        public Command NavigateToDetailTaskPageCommand { get; }
        
        public ObservableCollection<TaskObject> Tasks { get; set; }

        public async Task UpdateTasks(int idStatus = 0)
        {
            SqlDataReader getTasks = null;

            if (idStatus == 0)
                getTasks = await (new SqlCommand(
                    $"SELECT Requests.id, equipmentwork.started, o.number, equipmentwork.id, equipment.id, suppliername, equipment.name, e_status.statusname, etype.typename, u.FIO, comments, datecreation, commentsadmin, request_status.name, dateclose FROM Requests JOIN Users u on Requests.id_users = u.Id JOIN Office O on u.id_office = O.id JOIN Equipmentwork equipmentwork on Requests.id_equipmentwork = equipmentwork.id JOIN Equipmentstatus e_status on equipmentwork.id_status = e_status.id JOIN Requestsstatus request_status on Requests.id_requestsstatus = request_status.id JOIN Equipment equipment on equipmentwork.id_equipment = equipment.id JOIN Equipmenttype etype on equipment.id_type = etype.id JOIN Suppliers supplier on equipment.id_supplier = supplier.id",
                    Globals.connection)).ExecuteReaderAsync();
            else
                getTasks = await (new SqlCommand(
                    $"SELECT Requests.id, equipmentwork.started, o.number, equipmentwork.id, equipment.id, suppliername, equipment.name, e_status.statusname, etype.typename, u.FIO, comments, datecreation, commentsadmin, request_status.name, dateclose FROM Requests JOIN Users u on Requests.id_users = u.Id JOIN Office O on u.id_office = O.id JOIN Equipmentwork equipmentwork on Requests.id_equipmentwork = equipmentwork.id JOIN Equipmentstatus e_status on equipmentwork.id_status = e_status.id JOIN Requestsstatus request_status on Requests.id_requestsstatus = request_status.id JOIN Equipment equipment on equipmentwork.id_equipment = equipment.id JOIN Equipmenttype etype on equipment.id_type = etype.id JOIN Suppliers supplier on equipment.id_supplier = supplier.id WHERE Requests.id_requestsstatus = {idStatus}",
                    Globals.connection)).ExecuteReaderAsync();

            var tmpTasks = new List<TaskObject>();

            if (getTasks.HasRows)
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
                            TimeEnd = getTasks.GetValue(14) is DBNull ? DateTime.Now : getTasks.GetDateTime(14)
                        });
                }

            Tasks.Clear();
            foreach (var task in tmpTasks)
                Tasks.Add(task);

            getTasks.Close();
        }

        private async Task ExecuteNavigateToDetailTaskPageCommand(TaskObject model)
        {
            await App.NavigationPage.PushAsync(new DetailTask(model, this));
        }
    }
}