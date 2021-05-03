using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using NumberBomb.Enums;
using NumberBomb.Helper;
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
    public WinPageViewModel(int chancesRemaining, string gamerTagName)
    {
      finalScore = chancesRemaining;
      nameTag = gamerTagName;
      RestartCommand = new Command(RestartCommandExecute);
      MainCommand = new Command(MainCommandExecute);
      YesCommand = new Command(YesCommandExcute);
      NoCommand = new Command(NoCommandExcute);
      PostApiCall();
      if (Preferences.ContainsKey("numberOfVisit"))
      {
        NumberOfVisit = Preferences.Get("numberOfVisit", NumberOfVisit);
      }
      else
      {
        NumberOfVisit = 1;
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
      var scoreItems = JsonConvert.DeserializeObject<List<ScoreItem>>(result);
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
