using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class LosePage : ContentPage
    {
        public LosePage()
        {
            InitializeComponent();

            BindingContext = new LosePageViewModel();

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
