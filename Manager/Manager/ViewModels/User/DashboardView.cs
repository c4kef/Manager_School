using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Devices = new ObservableCollection<Device>(new List<Device>()
            {
                new Device()
                {
                    AttachDate = DateTime.Now,
                    Cabinet = "213",
                    Id = 0,
                    Manufacturer = "Щербенский завод",
                    Model = "АБВГД",
                    Status = "В работе",
                    Type = "Компьютер"
                },new Device()
                {
                    AttachDate = DateTime.Now,
                    Cabinet = "213",
                    Id = 0,
                    Manufacturer = "Щербенский завод",
                    Model = "АБВГД",
                    Status = "В работе",
                    Type = "Компьютер"
                },new Device()
                {
                    AttachDate = DateTime.Now,
                    Cabinet = "213",
                    Id = 0,
                    Manufacturer = "Щербенский завод",
                    Model = "АБВГД",
                    Status = "В работе",
                    Type = "Компьютер"
                },new Device()
                {
                    AttachDate = DateTime.Now,
                    Cabinet = "213",
                    Id = 0,
                    Manufacturer = "Щербенский завод",
                    Model = "АБВГД",
                    Status = "В работе",
                    Type = "Компьютер"
                },new Device()
                {
                    AttachDate = DateTime.Now,
                    Cabinet = "213",
                    Id = 0,
                    Manufacturer = "Щербенский завод",
                    Model = "АБВГД",
                    Status = "В работе",
                    Type = "Компьютер"
                },new Device()
                {
                    AttachDate = DateTime.Now,
                    Cabinet = "213",
                    Id = 0,
                    Manufacturer = "Щербенский завод",
                    Model = "АБВГД",
                    Status = "В работе",
                    Type = "Компьютер"
                },
            });
            
            NavigateToDetailDevicePageCommand = new Command<Device>(async (model) => await ExecuteNavigateToDetailDevicePageCommand(model));
            NavigateToDetailTaskPageCommand = new Command<TaskObject>(async (model) => await ExecuteNavigateToDetailTaskPageCommand(model));
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