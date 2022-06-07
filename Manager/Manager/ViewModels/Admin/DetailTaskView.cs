using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Manager.Views.Admin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.ViewModels.Admin
{
    public class DetailTaskView : ViewModelBase
    {
        private DetailTask _view;
        private TasksView _tasksView;
        
        public DetailTaskView(TaskObject task, DetailTask link, TasksView tasksView)
        {
            Task = task;
            _view = link;
            _tasksView = tasksView;
            
            switch (task.Status)
            {
                case "Отклонён":
                    Decline = true;
                    break;
                case "В работе":
                    Accept = true;
                    break;
                case "Завершён":
                    End = true;
                    break;
            }

            Comment = task.Comment;
            
            SendReport = new Command(async () => await ExecuteSendReport());
        }
        
        public TaskObject Task { get; set; }
        
        public Command SendReport { get; }

        private bool _accept;
        public bool Accept
        {
            get
            {
                return _accept;
            }
            set
            {
                _accept = value;
                OnPropertyChanged("Accept");
            }
        }
        
        private bool _decline;
        public bool Decline
        {
            get
            {
                return _decline;
            }
            set
            {
                _decline = value;
                OnPropertyChanged("Decline");
            }
        }
        
        private bool _end;
        public bool End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                OnPropertyChanged("End");
            }
        }
        
        public string Comment { get; set; }

        private async Task ExecuteSendReport()
        {
            if (string.IsNullOrEmpty(Comment) || (!Accept && !Decline && !End))
                return;

            var taskReady = new TaskObject()
            {
                Id = Task.Id,
                Comment = Comment,
                Description = Task.Description,
                Device = Task.Device,
                Status = Task.Status,
                Title = Task.Title,
                TimeEnd = Task.TimeEnd,
                TimeFrom = Task.TimeFrom
            };

            await (new SqlCommand(
                $"UPDATE Requests SET id_users = (Select Users.Id from Users WHERE Users.FIO = N'{taskReady.Device.UserName}'), id_equipmentwork = (Select Equipmentwork.id from Equipmentwork WHERE Equipmentwork.id = {Task.Device.WId}), id_requestsstatus = (Select Requestsstatus.id from Requestsstatus WHERE Requestsstatus.name = N'{GetStatusText()}'), commentsadmin = N'{taskReady.Comment}', datecreation = '{DateTime.Now}', datechange = '{DateTime.Now}' WHERE id = {taskReady.Id}",
                Globals.connection)).ExecuteNonQueryAsync();

            await _view.DisplayAlert("Отправлено", "Ваша заявка отправлена", "OK");

            await _tasksView.UpdateTasks(_tasksView.SelectedStatusIndex);
            
            string GetStatusText()
            {
                if (Accept)
                    return "В работе";
                else if (Decline)
                    return "Отклонён";
                else
                    return "Завершён";
            }
        }
    }
}