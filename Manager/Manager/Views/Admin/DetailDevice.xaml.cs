using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.ViewModels.Admin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manager.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailDevice : ContentPage
    {
        public DetailDevice(Device device)
        {
            InitializeComponent();

            BindingContext = new DetailDeviceView(device, this);
        }
    }
}