using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using NumberBomb.ViewModels;

namespace NumberBomb
{
    public partial class MainPage : ContentPage
    { 
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new GamerTagViewModel();
        }
    }
}
