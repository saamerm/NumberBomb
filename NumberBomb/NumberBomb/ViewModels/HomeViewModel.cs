using MediaManager;
using Microsoft.AppCenter.Crashes;
using NumberBomb.Helper;
using NumberBomb.Interfaces;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
  public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand StartButtonCommand { get; set; }
    public ICommand LeaderBoardButtonCommand { get; set; }
    public ICommand UserIcon_OnTapped { get; set; }
    public ICommand InfoIcon_OnTapped { get; set; }
    public ICommand PlayCommand { get; set; }
    private string _buttonText;
    public string ButtonText
    {
      get
      {
        return _buttonText;
      }
      set
      {
        _buttonText = value;
        if (PropertyChanged != null)
        {
          PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
        }
      }
    }
    private bool _isPlaying;
    public bool IsPlaying
    {
      get
      {
        return _isPlaying;
      }
      set
      {
        _isPlaying = value;
        if (PropertyChanged != null)
        {
          PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsPlaying)));
        }
      }
    }
    private bool _isBusy;
    public bool IsBusy
    {
      get
      {
        return _isBusy;
      }
      set
      {
        _isBusy = value;
        if (PropertyChanged != null)
        {
          PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
        }
      }
    }

    public HomeViewModel()
    {
      StartButtonCommand = new Command(StartCommandExecute);
      LeaderBoardButtonCommand = new Command(LeaderBoardCommandExecute);
      UserIcon_OnTapped = new Command(UserIconCommandExecute);
      InfoIcon_OnTapped = new Command(InfoIconCommandExecute);
      PlayCommand = new Command(PlayCommandExcute);
      GetApiUrl();
      MessagingCenter.Subscribe<GamePageViewModel, bool>(this, "isPlaying", (sender, _isPlaying) =>
      {
        IsPlaying = _isPlaying;
        ButtonText = (_isPlaying) ? "Music: On" : "Music: Off";
      });
      if (Preferences.ContainsKey("playMusic"))
      {
        IsPlaying = Preferences.Get("playMusic", false);
        ButtonText = (IsPlaying) ? "Music: On" : "Music: Off";
   
      }
      else
      {
        IsPlaying = false;
        ButtonText = "Music: Off";
      }
    
        Device.StartTimer(new TimeSpan(0, 1, 0), () =>
        {
          // DependencyService.Get<IMediaService>().StarMusic();
          CheckMusic();
          return Preferences.Get("playMusic", false);
        });
    
    }
    private async void PlayCommandExcute(object obj)
    {
      var audio = CrossMediaManager.Current;
      if (!IsPlaying)
      {
        Preferences.Set("playMusic", true);
        IsPlaying = true;
        ButtonText = "Music: On";

        await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
      }
      else
      {
        Preferences.Set("playMusic", false);
        IsPlaying = false;
        ButtonText = "Music: Off";
        await audio.Stop();
      }
    }

    private void UserIconCommandExecute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new ChangeUserPage());
    }

    private void InfoIconCommandExecute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new InfoPage());
    }

    private void LeaderBoardCommandExecute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new LeaderboardPage());
    }

    private void StartCommandExecute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new DifficultyPage());
    }
    private async void GetApiUrl()
    {
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var url = Constants.Feedback_Api_Key;
          var client = new HttpClient();
          var response = await client.GetAsync(url);
          var result = await response.Content.ReadAsStringAsync();
          Helper.Constants.Api_Key = result;
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
    private async void CheckMusic()
    {
      if (Preferences.Get("playMusic", false))
      {
        var audio = CrossMediaManager.Current;
        await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
      }
    }
  }
}
