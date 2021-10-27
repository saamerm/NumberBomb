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
        public bool ShouldShow { get; set; }
        public bool ShouldHide { get; set; }

        public DifficultyViewModel()
        {
            DiffcultyCommand = new Command<string>(DifficultyCommandExcute);
            BackButtonClicked = new Command(BackButtonClickedCommandExecute);
            ShouldShow = (Preferences.Get("HighestEasyScore", 0) == 0) ? false : true;
            ShouldHide = (Preferences.Get("HighestMediumScore", 0) != 0) ? true : false;
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
