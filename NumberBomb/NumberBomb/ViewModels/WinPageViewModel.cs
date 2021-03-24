using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class WinPageViewModel
    {
        public int finalScore;
        public string nameTag;
        public ICommand RestartCommand { get; set; }
        public ICommand MainCommand { get; set; }

        public WinPageViewModel(int chancesRemaining, string gamerTagName)
        {
            finalScore = chancesRemaining;
            nameTag = gamerTagName;
            RestartCommand = new Command(RestartCommandExecute);
            MainCommand = new Command(MainCommandExecute);
            PostApiCall();
        }

        private async Task PostApiCall()
        {
            var url = "https://script.google.com/macros/s/AKfycbyYixMarhPEUj3UVzOxWne1bJnMZDOcXUn3dHA4UrP21L_qA673bCkeG6Liys7c6oj5PA/exec";
            var client = new HttpClient();
            var data = new ScoreItem { Name = nameTag, Score = finalScore };
            var jsonString = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(jsonString);
            var response = await client.PostAsync(url, requestContent);
            var result = await response.Content.ReadAsStringAsync();
            var scoreItems = JsonConvert.DeserializeObject<List<ScoreItem>>(result);
        }

        private void RestartCommandExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void MainCommandExecute(object obj)
        {
            App.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
