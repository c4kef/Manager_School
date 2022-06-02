using System;
using System.Collections.Generic;
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

        public TaskObject Task { get; set; }
    }
}