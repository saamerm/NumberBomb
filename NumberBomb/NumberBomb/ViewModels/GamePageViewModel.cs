using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        public string _number;
        public int RandomNumber;
        public int GuessedValue;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CheckCommand { get; set; }

        public string GuessEntry
        {
            get => _number;
            set
            {
                _number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GuessEntry)));
            }
        }

        public GamePageViewModel()
        {
            Random randomNumberGenrator = new Random();
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
        }
    }
}