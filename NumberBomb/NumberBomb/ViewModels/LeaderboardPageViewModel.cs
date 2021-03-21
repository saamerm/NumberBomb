using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NumberBomb.ViewModels
{
    public class LeaderboardPageViewModel : INotifyPropertyChanged
    {
        public List<ScoreItem> _scoreItems;
        public List<ScoreItem> ScoreItems {
            get => _scoreItems;
            set
            {
                _scoreItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScoreItems)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public LeaderboardPageViewModel()
        {
            GetApiCall();
        }

        private async Task GetApiCall()
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
