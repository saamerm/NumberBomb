using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();

        }

        public async void StartButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        public async void LeaderBoardButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeaderboardPage());
        }

        public void UserIcon_OnTapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ChangeUserPage());

        }

        public void InfoIcon_OnTapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());

        }
    }
}
