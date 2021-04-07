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
        }
    }
}
