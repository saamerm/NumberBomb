using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class LosePageViewModel : BaseViewModel
    {
        public ICommand RestartCommand { get; set; }
        public ICommand MainCommand { get; set; }

        public LosePageViewModel()
        {
            RestartCommand = new Command(RestartCommandExecute);
            MainCommand = new Command(MainCommandExecute);
        }

        private void RestartCommandExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void MainCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
