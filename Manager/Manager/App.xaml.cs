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
            Globals.connection = new SqlConnection($@"Server=u1477164.plsk.regruhosting.ru;Database=u1477164_diha22;User Id=u1477164_dinhem;Password=20031962Din;");//@$"Data Source=C4ke;Initial Catalog=footballclub1; Integrated Security = True; MultipleActiveResultSets=True");

            new Task(async () => await Globals.connection.OpenAsync()).Start();
            
            MainPage = NavigationPage = new NavigationPage( new MainPage() );
            
            /*
             dt_t = new DataTable();

            new SqlDataAdapter(new SqlCommand("SELECT * FROM tournaments", Globals.connection)).Fill(dt_t);
            foreach (DataRow row in dt_t.Rows)
                dialog.ListTours.Add((string)row.ItemArray[1]);

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await new SqlCommand($@"INSERT INTO gamesschedule (date, team, stadium, ID_tournament, result, ticket_count, revenue) VALUES (N'{dialog.Date.Date + dialog.Time.TimeOfDay}', N'{dialog.Team}', N'{dialog.Stadium}', (select ID_tournament from tournaments where name = N'{dialog.Tournaments}'), N'{dialog.ResultVal}', '0', '0')", Globals.connection).ExecuteNonQueryAsync();
                FillGrid();
            }
             */
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