using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class HomeViewModel
    {
        public ICommand StartButtonCommand { get; set; }
        public ICommand LeaderBoardButtonCommand { get; set; }
        public ICommand UserIcon_OnTapped { get; set; }
        public ICommand InfoIcon_OnTapped { get; set; }

        public HomeViewModel()
        {
            StartButtonCommand = new Command(StartCommandExecute);
            LeaderBoardButtonCommand = new Command(LeaderBoardCommandExecute);
            UserIcon_OnTapped = new Command(UserIconCommandExecute);
            InfoIcon_OnTapped = new Command(InfoIconCommandExecute);  
        }

        private void UserIconCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new ChangeUserPage());
        }

        private void InfoIconCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new InfoPage());
        }

        private void LeaderBoardCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new LeaderboardPage());
        }

        private void StartCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new GamePage());
        }     
    }
}
