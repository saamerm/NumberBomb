﻿using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();

            var GamePageViewModel = new GamePageViewModel();
            BindingContext = GamePageViewModel;

           if (Device.RuntimePlatform.Equals("Android"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            }
            else if (Device.RuntimePlatform.Equals("iOS"))
            {
                TitleLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            } 

            GamePageViewModel.OnCheckFailed = ((obj) =>
            {
                GuessEntry.Focus();
            });
        }

        protected override void OnAppearing()
        {
            
            GuessEntry.Focus();
            base.OnAppearing();
        }
    }
}
