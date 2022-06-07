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
    public partial class DetailTask : ContentPage
    {
        public DetailTask(TaskObject task)
        {
            InitializeComponent();
            BindingContext = new DetailTaskView(task, this);
        }
    }
}