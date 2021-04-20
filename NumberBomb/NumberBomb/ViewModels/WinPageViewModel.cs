﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
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
    private void OpenAppReviewPopup()
    {
      var dialog = new AppReviewPopupPage();
      dialog.BindingContext = this;
      PopupNavigation.Instance.PushAsync(dialog);
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
      var url = "https://script.google.com/macros/s/AKfycbwGUUx8-D7ABvmE47urkspg-_ygi4bag5-ts6kC4UbIvWTCbieHkE8w6nXoAaX1DnCEQQ/exec";
      var client = new HttpClient();
      var data = new ScoreItem { Name = nameTag, Score = finalScore };
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
