using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTask : ContentPage
    {
        public DetailTask(TaskObject task)
        {
            Task = task;
            
            InitializeComponent();
            BindingContext = this;
        }

        public bool IsCanDelete
        {
            get
            {
                return Task.Status != "На рассмотрений" && Task.Status != "В работе";
            }
        }

        public TaskObject Task { get; set; }

        private async void DeleteTask(object sender, EventArgs e)
        {
            await (new SqlCommand($"DELETE FROM Requests WHERE id = {Task.Id}", Globals.connection)).ExecuteNonQueryAsync();
            await DisplayAlert("Успешно", "Ваша заявка была удалена", "OK");
            await App.NavigationPage.Navigation.PopAsync();
        }
    }
}