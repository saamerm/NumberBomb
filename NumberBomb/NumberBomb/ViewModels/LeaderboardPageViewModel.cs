using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using NumberBomb.Enums;
using NumberBomb.Helper;
using Sharpnado.Tabs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
  public class LeaderboardPageViewModel : BaseViewModel, INotifyPropertyChanged
  {
    public List<ScoreItem> _scoreItems;
    public event PropertyChangedEventHandler PropertyChanged;

    private string _message;
    public string Message
    {
      get => _message;
      set
      {
        _message = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
      }
    }

    public int _score;
    public List<ScoreItem> ScoreItems
    {
      get => _scoreItems;
      set
      {
        _scoreItems = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScoreItems)));
      }
    }
    public string Difficulity { get; set; }
    private int _selectedViewModelIndex = 0;
    public int SelectedViewModelIndex
    {
      get => _selectedViewModelIndex;
      set
      {
        _selectedViewModelIndex = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedViewModelIndex)));
      }
    }
    public ICommand BackButtonClicked { get; set; }
    public ICommand TabSelectedCommand { get; set; }
    private bool _isBusy;
    public bool IsBusy
    {
      get => _isBusy;
      set
      {
        _isBusy = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
      }
    }
    public LeaderboardPageViewModel()
    {
      _score = Preferences.Get("HighestEasyScore", 0);
      GenerateMessage();
      ScoreItems = new List<ScoreItem>();
      BackButtonClicked = new Command(BackButtonClickedCommandExecute);
      TabSelectedCommand = new Command(TabSelectedCommandExcute);
      Task.Run(async () => await GetBoardData("Easy"));
    }

    private void GenerateMessage()
    {
      Message = "Your best score is " + _score + " !";
    }

    private async void TabSelectedCommandExcute(object obj)
    {
      var index = obj as TabHostView;
      SelectedViewModelIndex = index.SelectedIndex;
      if (SelectedViewModelIndex == 0)
      {
        Difficulity = "Easy";
        _score = Preferences.Get("HighestEasyScore", 0);
      }
      else if (SelectedViewModelIndex == 1)
      {
        Difficulity = "Medium";
        _score = Preferences.Get("HighestMediumScore", 0);
      }
      else
      {
        Difficulity = "Hard";
        _score = Preferences.Get("HighestHardScore", 0);
      }
      GenerateMessage();
      ScoreItems.Clear();
      await GetBoardData(Difficulity);
    }

    private void BackButtonClickedCommandExecute(object obj)
    {
      Application.Current.MainPage.Navigation.PopAsync();
    }
    private async Task GetBoardData(string difficulity)
    {

      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var url = Constants.Api_Key + "?difficuiltyLevel=" + difficulity;
          var client = new HttpClient();
          var response = await client.GetAsync(url);
          var result = await response.Content.ReadAsStringAsync();
          var scoreItemsArray = JsonConvert.DeserializeObject<ScoreItem[]>(result);
          ScoreItems = new List<ScoreItem>(scoreItemsArray);

          IsBusy = false;
        }
      }
      catch (Exception e)
      {
        IsBusy = false;
        Crashes.TrackError(e);

      }
      finally
      {
        IsBusy = false;
      }
    }
  }
}
