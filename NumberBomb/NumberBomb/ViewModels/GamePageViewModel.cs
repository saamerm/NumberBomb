using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        public string _number;
        public string _text;
        public int RandomNumber;
        public int GuessedValue;
        public int _chances = 10;
        public int remainNumber;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CheckCommand { get; set; }

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
            Random randomNumberGenrator = new Random();
            _text = "Roboto, you have to find the key to stop the bomb from blowing up. You have to guess a number between 1-100, The correct number will give you the key to defuse the bomb \n But you only have 10 chances to guess it right!";
            RandomNumber = randomNumberGenrator.Next(100) + 1;
            if (GuessEntry == null)
            {
                GuessEntry = "0";
            }
            CheckCommand = new Command(CheckCommandExecute);
        }

        private void CheckCommandExecute(object obj)
        {
            GuessedValue = Convert.ToInt32(GuessEntry);
            if (RandomNumber == GuessedValue)
            {
                App.Current.MainPage.Navigation.PushAsync(new WinPage());
            }
            else
            {
                if(GuessedValue < RandomNumber)
                {
                    Hint = "Roboto, " + GuessEntry + " is not the key \n Please enter a number between " + GuessEntry + " - 100";
                }
                else
                {
                    Hint = "Roboto, " + GuessEntry + " is not the key \n Please enter a number between 1 - " + GuessEntry;
                }

                if (ChancesRemaining == 1)
                {
                    if (RandomNumber != GuessedValue)
                    {
                        App.Current.MainPage.Navigation.PushAsync(new LosePage());

                    }
                }
                else
                {
                    ChancesRemaining -= 1;
                }               
            }
        }
    }
}