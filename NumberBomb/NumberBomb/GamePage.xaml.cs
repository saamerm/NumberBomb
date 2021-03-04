using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NumberBomb
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform.Equals("Android"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.StartAndExpand;
            }
            else if (Device.RuntimePlatform.Equals("iOS"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.EndAndExpand;
            }
        }
    }
}
