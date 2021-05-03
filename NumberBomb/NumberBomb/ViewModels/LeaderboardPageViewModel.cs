using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
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
    public string message { get; set; }
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
      _score = Preferences.Get("_chances", 0);
      ScoreItems = new List<ScoreItem>();
      BackButtonClicked = new Command(BackButtonClickedCommandExecute);
      TabSelectedCommand = new Command(TabSelectedCommandExcute);
      GetBoardData("Easy");
      message = "Your best score is " + _score + " !";
    }

    private void TabSelectedCommandExcute(object obj)
    {
      var index = obj as TabHostView;
      SelectedViewModelIndex = index.SelectedIndex;
      if (SelectedViewModelIndex == 0)
      {
        Difficulity = "Easy";
      }
      else if (SelectedViewModelIndex == 1)
      {
        Difficulity = "Medium";
      }
      else
      {
        Difficulity = "Hard";
      }
      ScoreItems.Clear();
      GetBoardData(Difficulity);
    }

    private void BackButtonClickedCommandExecute(object obj)
    {
      Application.Current.MainPage.Navigation.PopAsync();
    }
    private async void GetBoardData(string difficulity)
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
