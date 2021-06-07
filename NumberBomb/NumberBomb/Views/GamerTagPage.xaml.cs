using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class GamerTagPage : ContentPage
    {
        public GamerTagPage()
        {
            InitializeComponent();

            BindingContext = new GamerTagViewModel();
        }
    }
}
