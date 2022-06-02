using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTask : ContentPage
    {
        public DetailTask(TaskObject task)
        {
            Task = task;
            
            InitializeComponent();
            BindingContext = this;
            
            SendReport = new Command(async () => await ExecuteSendReport());
        }

        public TaskObject Task { get; set; }
        
        public Command SendReport { get; }
        
        private async Task ExecuteSendReport()
        {
            await DisplayAlert("Отправлено", "Ваша заявка отправлена", "OK");
        }
    }
}