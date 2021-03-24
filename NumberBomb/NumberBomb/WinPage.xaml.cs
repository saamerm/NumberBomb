using System;
using System.Collections.Generic;
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

            label.Text = "Your remaining chances was " + chancesRemaining + " and your highest score is " + score + ".";
        }
    }
}
