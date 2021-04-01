using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace NumberBomb
{
    public partial class App : Application
    {
        public string GamerTag;

        public App()
        {
            InitializeComponent();
            GamerTag = Preferences.Get("NameTag", string.Empty);

            if (string.IsNullOrEmpty(GamerTag))
            {
                MainPage = new GamerTagPage();
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=a1028848-3b8c-4c05-b99c-ca2bbf851bf8;" +
                  "uwp={Your UWP App secret here};" +
                  "android=0aa22654-3b43-49f1-9d6f-9f7c72c157e8;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
