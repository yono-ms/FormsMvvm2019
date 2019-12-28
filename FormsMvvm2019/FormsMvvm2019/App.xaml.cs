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

            MainPage = new MainPage();
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
