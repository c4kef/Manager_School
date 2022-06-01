using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new DashboardView();
        }
    }
}