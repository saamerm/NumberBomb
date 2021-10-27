using System;
using System.Windows.Input;
using NumberBomb.Enums;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class DifficultyViewModel : BaseViewModel
    {
        public ICommand DiffcultyCommand { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public bool ShouldShowMedium { get; set; }
        public bool ShouldShowHard { get; set; }

        public DifficultyViewModel()
        {
            DiffcultyCommand = new Command<string>(DifficultyCommandExcute);
            BackButtonClicked = new Command(BackButtonClickedCommandExecute);
            ShouldShowMedium = (Preferences.Get("HighestEasyScore", 0) == 0) ? false : true;
            ShouldShowHard = (Preferences.Get("HighestMediumScore", 0) == 0) ? false : true;
        }

        private void BackButtonClickedCommandExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void DifficultyCommandExcute(string selectedDiffculty)
        {

            if (selectedDiffculty == Difficulty.Easy.ToString())
            {
                Preferences.Set("difficulty", Difficulty.Easy.ToString());
            }
            else if (selectedDiffculty == Difficulty.Medium.ToString())
            {
                Preferences.Set("difficulty", Difficulty.Medium.ToString());
            }
            else
            {
                Preferences.Set("difficulty", Difficulty.Hard.ToString());
            }
            App.Current.MainPage.Navigation.PushAsync(new TutorialPage());
        }

        private void EasyCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new TutorialPage());
        }
    }
}
