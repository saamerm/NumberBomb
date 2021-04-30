using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class ChangeUserPage : ContentPage
    {
        public ChangeUserPage()
        {
            InitializeComponent();

            BindingContext = new ChangeUserViewModel();
        }
    }
}
