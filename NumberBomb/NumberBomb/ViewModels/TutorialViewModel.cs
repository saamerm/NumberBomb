using System;
using System.ComponentModel;
using System.Windows.Input;
using NumberBomb.Enums;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class TutorialViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string _text;
        public string _gamerTagName;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OkCommand { get; set; }
        public ICommand BackButtonClicked { get; set; }

        public string Hint
        {
            get => _text;
            set
            {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hint)));
            }
        }
        public TutorialViewModel()
        {
            _gamerTagName = Preferences.Get("NameTag", string.Empty);
            CheckDifficultyLevel();
            OkCommand = new Command(OkCommandExecute);
            BackButtonClicked = new Command(BackButtonClickedCommandExecute);
        }

        private async void BackButtonClickedCommandExecute(object obj)
        {
            var response = await App.Current.MainPage.DisplayAlert("Are you sure you want to exit?", "", "Yes", "No");
            if (response == true)
            {
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void CheckDifficultyLevel()
        {
            var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());

            if (difficulitylevel == (Difficulty.Easy.ToString()))
            {
                _text = _gamerTagName + ", you have to find the correct number key to stop the bomb from blowing up.";
            }
            else if (difficulitylevel == Difficulty.Medium.ToString())
            {
                _text = _gamerTagName + ", there are 5 number bombs hiding in between the numbers, try to avoid them and guess the correct number key.";
            }
            else
            {
                _text = _gamerTagName + ", there are 5 number bombs hiding in between the numbers. You have 5 chances to guess the correct number key.";
            }

        }

        private void OkCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new GamePage());
        }
    }
}
