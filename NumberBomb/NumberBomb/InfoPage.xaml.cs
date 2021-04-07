using NumberBomb.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NumberBomb
{
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
            BindingContext = new InfoViewModel();
            if (Device.RuntimePlatform.Equals("Android"))
            {
                TitleLabel.HorizontalTextAlignment = TextAlignment.Start;
            }
            else if (Device.RuntimePlatform.Equals("iOS"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.Center;
            }
        }
    }
}
