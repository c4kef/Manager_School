using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.ViewModels.Admin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tasks : ContentPage
    {
        public Tasks()
        {
            InitializeComponent();
            BindingContext = new TasksView();
        }
    }
}