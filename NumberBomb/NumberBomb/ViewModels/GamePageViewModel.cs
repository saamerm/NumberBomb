using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        public string _number;
        public string _text;
        public string _gamerTagName;
        public int _score;
        public int _newScore;
        public int RandomNumber;
        public int GuessedValue;
        public int _chances = 10;
        public int remainNumber;
        public event PropertyChangedEventHandler PropertyChanged;
        public int _minimum;
        public int _maximum;
        public ICommand CheckCommand { get; set; }
        public ICommand RefreshIcon_OnTapped { get; set; }
        public ICommand BackButtonClicked { get; set; }
        Random randomNumberGenrator;

        public int MaximumLimit
        {
            get => _maximum;
            set
            {
                _maximum = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaximumLimit)));
            }
        }

        public int MinimumLimit
        {
            get => _minimum;
            set
            {
                _minimum = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinimumLimit)));
            }
        }

        public string Hint
        {
            get => _text;
            set
            {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hint)));
            }
        }

        public string GuessEntry
        {
            get => _number;
            set
            {
                _number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GuessEntry)));
            }
        }

         public int ChancesRemaining
        {
            get => _chances;
            set
            {
                _chances = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChancesRemaining)));
            }
        }

        public GamePageViewModel()
        {
            randomNumberGenrator = new Random();
            MinimumLimit = 1;
            MaximumLimit = 100;
            _score = Preferences.Get("_chances", 0);
            _gamerTagName = Preferences.Get("NameTag", string.Empty);
            _text = _gamerTagName + ", you have to guess a number between 1-100, The correct number will give you the key to defuse the bomb \n But you only have 10 chances to guess it right!";
            RandomNumber = randomNumberGenrator.Next(100) + 1;
            if (GuessEntry == null)
            {
                GuessEntry = "0";
            }
            CheckCommand = new Command(CheckCommandExecute);
            RefreshIcon_OnTapped = new Command(RefreshIconCommandExecute);
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

        private void Reset()
        {
            MaximumLimit = 100;
            MinimumLimit = 1;
            RandomNumber = randomNumberGenrator.Next(100) + 1;
            _chances = 10;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChancesRemaining)));
            _text = _gamerTagName + ", you have to guess a number between 1-100, The correct number will give you the key to defuse the bomb \n But you only have 10 chances to guess it right!";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hint)));
            _number = "0";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GuessEntry)));
        }

        private async void RefreshIconCommandExecute(object obj)
        {
            var response = await App.Current.MainPage.DisplayAlert("Do you want to restart?", "", "Restart", "Cancel");
            if(response == true)
            {
                Reset();
            }
        }

        private void CheckCommandExecute(object obj)
        {
            GuessedValue = Convert.ToInt32(GuessEntry);

            if (RandomNumber == GuessedValue)
            {
                _score = Preferences.Get("_chances", 0);
                if (ChancesRemaining > _score)
                {
                    Preferences.Set("_chances", ChancesRemaining);
                    _newScore = Preferences.Get("_chances", 0);
                }
                else
                {
                    _newScore = Preferences.Get("_chances", 0);
                }
               
                App.Current.MainPage.Navigation.PushAsync(new WinPage(ChancesRemaining, _newScore, _gamerTagName));
                Reset();
            }
            else
            {
                IncorrectInput();
            }
        }

        private void IncorrectInput()
        {
            if (GuessedValue < RandomNumber)
            {
                MinimumLimit = GuessedValue;
                Hint = _gamerTagName + ", " + GuessEntry + " is not the key \n Please enter a number between " + MinimumLimit + " - " + MaximumLimit;
            }
            else
            {
                MaximumLimit = GuessedValue;
                Hint = _gamerTagName + ", " + GuessEntry + " is not the key \n Please enter a number between " + MinimumLimit + " - " + MaximumLimit;
            }

            if (ChancesRemaining == 1)
            {
                if (RandomNumber != GuessedValue)
                {
                    App.Current.MainPage.Navigation.PushAsync(new LosePage());
                    Reset();
                }
            }
            else
            {
                ChancesRemaining -= 1;
            }
        }
    }
}