using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Manager
{
    public partial class App : Application
    {
        public static NavigationPage NavigationPage;
        
        public App()
        {
            InitializeComponent();
            
            Globals.connection = new SqlConnection($@"Server=u1477164.plsk.regruhosting.ru;Database=u1477164_diha22;User Id=u1477164_dinhem;Password=20031962Din;MultipleActiveResultSets=true;");

            new Task(async () => await Globals.connection.OpenAsync()).Start();
            
            MainPage = NavigationPage = new NavigationPage( new MainPage() );
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}