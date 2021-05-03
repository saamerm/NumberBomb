using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaManager;
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
        public string _pauseimage;
        public event PropertyChangedEventHandler PropertyChanged;
        public int _minimum;
        public int _maximum;
        public ICommand PlayCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public ICommand RefreshIcon_OnTapped { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public Action<bool> OnCheckFailed { get; set; }
        public bool IsPlaying { get; set; }
        public string LoseMessage { get; set; }
        Random _randomNumberGenrator;

        public string PauseImage
        {
            get => _pauseimage;
            set
            {
                _pauseimage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PauseImage)));
            }
        }
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
            PlayCommand = new Command(PlayCommandExcute);
            CheckCommand = new Command(CheckCommandExecute);
            RefreshIcon_OnTapped = new Command(RefreshIconCommandExecute);
            BackButtonClicked = new Command(BackButtonClickedCommandExecute);

            if (Preferences.ContainsKey("playMusic"))
            {
                IsPlaying = Preferences.Get("playMusic", false);
                PauseImage = (IsPlaying) ? "volume_up_24px.png" : "volume_off_24px.png";
            }
            if (IsPlaying)
            {
                RepeateMusic();
            }
               
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

        private async void PlayCommandExcute(object obj)
        {
            if (!IsPlaying)
            {
                Preferences.Set("playMusic", true);
                PauseImage = "volume_up_24px.png";
                IsPlaying = true;
                var audio = CrossMediaManager.Current;
                await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
            }
            else
            {
                PauseImage = "volume_off_24px.png";
                IsPlaying = false;
                Preferences.Set("playMusic", false);
                await CrossMediaManager.Current.Stop();
            }
        }

        private async void BackButtonClickedCommandExecute(object obj)
        {
            var response = await App.Current.MainPage.DisplayAlert("Are you sure you want to exit?", "", "Yes", "No");
            if (response == true)
            {
                var isPlaying = Preferences.Get("playMusic", false);
                MessagingCenter.Send<GamePageViewModel, bool>(this, "isPlaying", isPlaying);
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private void Reset()
        {
            MaximumLimit = 100;
            MinimumLimit = 1;
            RandomNumber = _randomNumberGenrator.Next(100) + 1;
            SetChances();
            CloudImage = "cloud.png";
            Hint = "Please enter a number between 1 - 100";
            GuessEntry = "";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private async void RefreshIconCommandExecute(object obj)
        {
            if (ChancesRemaining < 10)
            {
                var response = await App.Current.MainPage.DisplayAlert("Do you want to restart?", "", "Restart", "Cancel");
                if (response == true)
                {
                    Reset();
                }
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
                await CheckGuess();
            }
            GuessEntry = "";

            // Chances Remaining resets to 10 as soon
            // as the user loses or wins the page, so we dont
            // want the popup to appear at that time
            if (ChancesRemaining != 10)
                OnCheckFailed?.Invoke(true);
        }

        private async Task CheckGuess()
        {
            PreviousGuess = GuessedValue;
            if (RandomNumber == GuessedValue)
            {
                CorrectInput();
            }
            else
            {
                await IncorrectInput();
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

        private async Task IncorrectInput()
        {
            //if medium or hard and the user chose one of the 5 wrong number push to the losepage

            var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());
            if (difficulitylevel == (Difficulty.Medium.ToString()) || difficulitylevel == (Difficulty.Hard.ToString()))
            {
                if(GuessedValue == IncorrectNumber1 || GuessedValue == IncorrectNumber2 || GuessedValue == IncorrectNumber3 || GuessedValue == IncorrectNumber4 || GuessedValue == IncorrectNumber5)
                {
                    LoseMessage = "Unfortunately " + GuessedValue + " was one of the number bombs";
                    App.Current.MainPage.Navigation.PushAsync(new LosePage(LoseMessage));
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
                    LoseMessage = "Oops! You did not find the key this time";
                    await App.Current.MainPage.Navigation.PushAsync(new LosePage(LoseMessage));
                    Reset();
                }
            }
            else
            {
                ChancesRemaining -= 1;
            }
        }
        private void RepeateMusic()
        {
            Device.StartTimer(new TimeSpan(0, 3, 3), () =>
               {
                   var audio = CrossMediaManager.Current;
                   audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
                   return IsPlaying;
               });
        }
    }
}