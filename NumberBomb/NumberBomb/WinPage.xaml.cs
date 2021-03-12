using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class WinPage : ContentPage
    {
        public int _chances;
        public int newScore;

        public WinPage(int chancesRemaining, int score)
        {
            InitializeComponent();

            BindingContext = new WinPageViewModel();

            _chances = chancesRemaining;
            newScore = score;

            label.Text = "Your remaining chances was " +  _chances + " and your highest score is " + newScore + ".";


            if (Device.RuntimePlatform.Equals("Android"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.StartAndExpand;
            }
            else if (Device.RuntimePlatform.Equals("iOS"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            }
        }
    }
}
