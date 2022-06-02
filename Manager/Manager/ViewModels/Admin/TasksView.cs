using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Manager.Views.Admin;
using Xamarin.Forms;

namespace Manager.ViewModels.Admin
{
    public class TasksView : ViewModelBase
    {
        public TasksView()
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
            
            NavigateToDetailTaskPageCommand = new Command<TaskObject>(async (model) => await ExecuteNavigateToDetailTaskPageCommand(model));
        }
        
        public Command NavigateToDetailTaskPageCommand { get; }
        
        public ObservableCollection<TaskObject> Tasks { get; set; }
        
        private async Task ExecuteNavigateToDetailTaskPageCommand(TaskObject model)
        {
            await App.NavigationPage.PushAsync(new DetailTask(model));
        }
    }
}