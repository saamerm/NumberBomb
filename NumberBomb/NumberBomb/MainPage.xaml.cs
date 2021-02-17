using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace NumberBomb
{
    public partial class MainPage : ContentPage
    { 
        public MainPage()
        {
            InitializeComponent();
            NameEntry.Text = Preferences.Get("NameTag", string.Empty);
        }

        public async void OnButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("NameTag", NameEntry.Text);
        }
    }
}
