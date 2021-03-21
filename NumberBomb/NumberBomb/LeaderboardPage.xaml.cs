using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class LeaderboardPage : ContentPage
    {
        public LeaderboardPage()
        {
            InitializeComponent();

            BindingContext = new LeaderboardPageViewModel();
        }
    }
}
