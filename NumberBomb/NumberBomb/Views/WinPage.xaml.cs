using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class WinPage : ContentPage
    {

        WinPageViewModel _viewModel;
        public WinPage(int chancesRemaining, int score, string gamerTagName)
        {
            InitializeComponent();

            _viewModel = new WinPageViewModel(chancesRemaining, gamerTagName);
            BindingContext = _viewModel;     

            label.Text = "Your remaining chances was " + chancesRemaining;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(2000);
            await AnimationConfetti.FadeTo(1,500);
            await WinLayout.FadeTo(1, 2000);
        }
    }
}
