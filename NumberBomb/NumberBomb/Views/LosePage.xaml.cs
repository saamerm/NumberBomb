using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class LosePage : ContentPage
    {
        public LosePage(string message)
        {
            InitializeComponent();

            BindingContext = new LosePageViewModel();

            messagelabel.Text = message;
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(2000);
            await AnimationView.FadeTo(0, 500);
            await LoseLayout.FadeTo(1, 2000);
        }
    }
}
