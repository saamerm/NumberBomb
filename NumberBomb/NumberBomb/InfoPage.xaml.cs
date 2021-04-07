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

            if (Device.RuntimePlatform.Equals("Android"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            }
            else if (Device.RuntimePlatform.Equals("iOS"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            }
        }
    }
}
