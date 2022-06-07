using System;
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
        
        public DetailTaskView(TaskObject task, DetailTask link)
        {
            Task = task;
            _view = link;

            switch (task.Status)
            {
                case "Отклонён":
                    Decline = true;
                    break;
                case "В работе":
                case "Завершён":
                    Accept = true;
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
        
        public string Comment { get; set; }

        private async Task ExecuteSendReport()
        {
            if (string.IsNullOrEmpty(Comment) || (!Accept && !Decline))
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
                $"UPDATE Requests SET id_users = (Select Users.Id from Users WHERE Users.FIO = N'{taskReady.Device.UserName}'), id_equipmentwork = (Select Equipmentwork.id from Equipmentwork WHERE Equipmentwork.id = {Task.Device.WId}), id_requestsstatus = (Select Requestsstatus.id from Requestsstatus WHERE Requestsstatus.name = N'{((Decline) ? "Отклонён": "В работе")}'), commentsadmin = N'{taskReady.Comment}', datecreation = '{DateTime.Now}', datechange = '{DateTime.Now}' WHERE id = {taskReady.Id}",
                Globals.connection)).ExecuteNonQueryAsync();

            await _view.DisplayAlert("Отправлено", "Ваша заявка отправлена", "OK");
        }
    }
}