using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class LeaderboardPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public List<ScoreItem> _scoreItems;
        public event PropertyChangedEventHandler PropertyChanged;
        public string message { get; set; }
        public int _score;
        public List<ScoreItem> ScoreItems {
            get => _scoreItems;
            set
            {
                _scoreItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScoreItems)));
            }
        }
       public ICommand BackButtonClicked { get; set; }
       public LeaderboardPageViewModel()
        {
            _score = Preferences.Get("_chances", 0);
            ScoreItems = new List<ScoreItem>();
            BackButtonClicked = new Command(BackButtonClickedCommandExecute);
            GetApiCall();
            message = "Your best score is " + _score + " !";            
        }

      private void BackButtonClickedCommandExecute(object obj)
      {
          Application.Current.MainPage.Navigation.PopAsync();
      }

    public async Task GetApiCall()
        {
            var url = "https://script.google.com/macros/s/AKfycbyYixMarhPEUj3UVzOxWne1bJnMZDOcXUn3dHA4UrP21L_qA673bCkeG6Liys7c6oj5PA/exec";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var scoreItemsArray = JsonConvert.DeserializeObject<ScoreItem[]>(result);
            ScoreItems = new List<ScoreItem>(scoreItemsArray);
            
        }
    }
}
