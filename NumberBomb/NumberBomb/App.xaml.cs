using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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
                MainPage = new HomePage();
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
