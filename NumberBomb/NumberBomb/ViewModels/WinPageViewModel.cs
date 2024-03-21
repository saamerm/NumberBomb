using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using NumberBomb.Enums;
using NumberBomb.Helper;
using NumberBomb.Models;
using Plugin.StoreReview;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
  public class WinPageViewModel : BaseViewModel
    {
    public int finalScore;
    public string nameTag;
    public ICommand RestartCommand { get; set; }
    public ICommand MainCommand { get; set; }
    public int NumberOfVisit { get; set; }
    public ICommand YesCommand { get; set; }
    public ICommand NoCommand { get; set; }
    public string ChangeText { get; set; }
    public string _levelWon;
    public string _levelReached;

    public WinPageViewModel(int chancesRemaining, string gamerTagName)
    {
      finalScore = chancesRemaining;
      nameTag = gamerTagName;
      RestartCommand = new Command(RestartCommandExecute);
      MainCommand = new Command(MainCommandExecute);
      YesCommand = new Command(YesCommandExcute);
      NoCommand = new Command(NoCommandExcute);
      ChangeText = "You have successfully defused the bomb";
      PostApiCall();
      if (Preferences.ContainsKey("numberOfVisit"))
      {
        NumberOfVisit = Preferences.Get("numberOfVisit", NumberOfVisit);
      }
      else
      {
        NumberOfVisit = 1;
      }

      // This value is set in the Difficulty Selection page, so we dont need to worry about this
      _levelWon = Preferences.Get("difficulty", Difficulty.Easy.ToString());
      _levelReached = Preferences.Get("LevelReached", "Easy");

      // If it's the first you win an easy game
      if (_levelWon == "Easy" && _levelReached == "Easy")
      {
        ChangeText = "You have unlocked Medium difficulty mode";
        Preferences.Set("LevelReached", "Medium");
      }
      else if(_levelWon == "Medium" && _levelReached == "Medium" )
      {
        ChangeText = "You have unlocked Hard difficulty mode";
        Preferences.Set("LevelReached", "Hard");
      }
      else if (_levelWon == "Hard" && _levelReached == "Hard")
      {
        ChangeText = "You have unlocked the Bonus round";
        Preferences.Set("LevelReached", "Bonus");
      }
      CheckNumberOfVisit();
    }

    private async Task OpenAppReviewPopup()
    {
       Analytics.TrackEvent("PageView: AppReviewPopup");
       var alert = await App.Current.MainPage.DisplayAlert(
                "", "You've saved the day, we'd love your feedback. Do you like the game?",
                "Yes",
                "No");
       if (alert)
       {
         Analytics.TrackEvent("SelectAction: AppReviewPopup-Like");
         YesCommandExcute(null);
       }
       else
         NoCommandExcute(null);
    }

    private void CheckNumberOfVisit()
    {
      if (NumberOfVisit == 1 || NumberOfVisit == 3 || NumberOfVisit == 10)
      {
        OpenAppReviewPopup();
      }
      NumberOfVisit++;
      Preferences.Set("numberOfVisit", NumberOfVisit);
    }
    private async Task PostApiCall()
    {
      var url = Constants.Api_Key;
      var client = new HttpClient();
      var data = new ScoreItem { Name = nameTag, Score = finalScore, Difficuilty = Preferences.Get("difficulty", Difficulty.Easy.ToString()) };
      var jsonString = JsonConvert.SerializeObject(data);
      var requestContent = new StringContent(jsonString);
      var response = await client.PostAsync(url, requestContent);
      var result = await response.Content.ReadAsStringAsync();
      Console.WriteLine(result);
      // TODO: Us this to enhance the message to check if the user is first
      //var scoreResponse = JsonConvert.DeserializeObject<PostScoreResponse>(result);
    }
    private void NoCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new FeedbackPage());
    }

    private async void YesCommandExcute(object obj)
    {
      await CrossStoreReview.Current.RequestReview(false);
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
