using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class TutorialPage : ContentPage
    {
        public TutorialPage()
        {
            InitializeComponent();

            BindingContext = new TutorialViewModel();

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
