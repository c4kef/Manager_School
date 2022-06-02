using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.ViewModels.Admin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace Manager.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new DashboardView();
        }

        private async void ScanResult(Result result)
        {
            await DisplayAlert("Результат", result.Text, "OK");
        }
    }
}