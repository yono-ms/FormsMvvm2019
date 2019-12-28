using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsMvvm2019
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppLogger.InitializeLog();

            MainPage = new NavigationPage(new SplashPage());
        }

        static AppDatabase database;

        public static AppDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new AppDatabase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
