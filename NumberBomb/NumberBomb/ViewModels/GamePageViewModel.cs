using System;
using System.ComponentModel;
using System.Windows.Input;
using NumberBomb.Enums;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class GamePageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string _number;
        public string _text;
        public string _gamerTagName;
        public string _image;
        public int _score;
        public int _newScore;
        public int RandomNumber;
        public int IncorrectNumber1;
        public int IncorrectNumber2;
        public int IncorrectNumber3;
        public int IncorrectNumber4;
        public int IncorrectNumber5;
        public int GuessedValue;
        private int PreviousGuess;
        public int _chances;
        public int remainNumber;
        public event PropertyChangedEventHandler PropertyChanged;
        public int _minimum;
        public int _maximum;
        public ICommand CheckCommand { get; set; }
        public ICommand RefreshIcon_OnTapped { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public Action<bool> OnCheckFailed { get; set; }
        Random _randomNumberGenrator;

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

        public string CloudImage
        {
            get => _image;
            set
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CloudImage)));
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
            _randomNumberGenrator = new Random();
            MinimumLimit = 1;
            MaximumLimit = 100;
            GenerateNumber();
            SetChances();
            _score = Preferences.Get("_chances", 0);
            CloudImage = "cloud.png";
            _gamerTagName = Preferences.Get("NameTag", string.Empty);
            _text = "Please enter a number between 1 - 100";
            CheckCommand = new Command(CheckCommandExecute);
            RefreshIcon_OnTapped = new Command(RefreshIconCommandExecute);
            BackButtonClicked = new Command(BackButtonClickedCommandExecute);
        }

        private void GenerateNumber()
        {
            var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());

            if (difficulitylevel == (Difficulty.Easy.ToString()))
            {
                RandomNumber = _randomNumberGenrator.Next(100) + 1;
            }
            else
            {
                RandomNumber = _randomNumberGenrator.Next(100) + 1;
                IncorrectNumber1 = _randomNumberGenrator.Next(100) + 1;
                IncorrectNumber2 = _randomNumberGenrator.Next(100) + 1;
                IncorrectNumber3 = _randomNumberGenrator.Next(100) + 1;
                IncorrectNumber4 = _randomNumberGenrator.Next(100) + 1;
                IncorrectNumber5 = _randomNumberGenrator.Next(100) + 1;
            }
        }

        private void SetChances()
        {
            var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());

            if (difficulitylevel == (Difficulty.Hard.ToString()))
            {
                ChancesRemaining = 5;
            }
            else
            {
                ChancesRemaining = 10;
            }
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
            RandomNumber = _randomNumberGenrator.Next(100) + 1;
            SetChances();
            CloudImage = "cloud.png";
            Hint = _gamerTagName + ", you have to guess a number between 1-100, The correct number will give you the key to defuse the bomb \n But you only have 10 chances to guess it right!";
            GuessEntry = "";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private async void RefreshIconCommandExecute(object obj)
        {
            var response = await App.Current.MainPage.DisplayAlert("Do you want to restart?", "", "Restart", "Cancel");
            if(response == true)
            {
                Reset();
            }
        }

        private async void CheckCommandExecute(object obj)
        {
            Int32.TryParse(GuessEntry, out GuessedValue);

            if (String.IsNullOrEmpty(GuessedValue.ToString()) || GuessedValue < 1 || GuessedValue > 100)
            {
                await Application.Current.MainPage.DisplayAlert("", "Please enter a number within the limits", "OK");
            }
            else if(GuessedValue == PreviousGuess)
            {
                await Application.Current.MainPage.DisplayAlert("", "Please enter a number different from the previous guess", "OK");
            }
            else
            {
                CheckGuess();
            }
            GuessEntry = "";
            OnCheckFailed?.Invoke(true);
        }

        private void CheckGuess()
        {
            PreviousGuess = GuessedValue;
            if (RandomNumber == GuessedValue)
            {
                CorrectInput();
            }
            else
            {
                IncorrectInput();
            }
        }

        private void CorrectInput()
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

        private void IncorrectInput()
        {
            //if medium or hard and the user chose one of the 5 wrong number push to the losepage

            var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());

            if (difficulitylevel == (Difficulty.Medium.ToString()) || difficulitylevel == (Difficulty.Hard.ToString()))
            {
                if(GuessedValue == IncorrectNumber1 || GuessedValue == IncorrectNumber2 || GuessedValue == IncorrectNumber3 || GuessedValue == IncorrectNumber4 || GuessedValue == IncorrectNumber5)
                {
                    App.Current.MainPage.Navigation.PushAsync(new LosePage());
                    Reset();
                    return;
                }
            }

            if (GuessedValue < RandomNumber)
                MinimumLimit = GuessedValue;
            else
                MaximumLimit = GuessedValue;

            CloudImage = "darkcloud.png";                
            Hint = _gamerTagName + ", " + GuessEntry + " is not the key \n Please enter a number between " + MinimumLimit + " - " + MaximumLimit;

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