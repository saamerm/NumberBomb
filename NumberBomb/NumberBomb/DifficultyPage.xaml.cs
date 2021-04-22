using System;
using System.Collections.Generic;
using NumberBomb.ViewModels;
using Xamarin.Forms;

namespace NumberBomb
{
    public partial class DifficultyPage : ContentPage
    {
        public DifficultyPage()
        {
            InitializeComponent();
            BindingContext = new DifficultyViewModel();

        }
    }
}
