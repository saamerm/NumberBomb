using MediaManager;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
  public class HomeViewModel : INotifyPropertyChanged
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
    public HomeViewModel()
    {
      StartButtonCommand = new Command(StartCommandExecute);
      LeaderBoardButtonCommand = new Command(LeaderBoardCommandExecute);
      UserIcon_OnTapped = new Command(UserIconCommandExecute);
      InfoIcon_OnTapped = new Command(InfoIconCommandExecute);
      PlayCommand = new Command(PlayCommandExcute);
      ButtonText = "Sound on";
    }

    private async void PlayCommandExcute(object obj)
    {
      if (!IsPlaying)
      {
        Preferences.Set("playMusic", true);
        IsPlaying = true;
        var audio = CrossMediaManager.Current;
        ButtonText = "Sound on";
        await audio.PlayFromAssembly("music.mp3", typeof(HomeViewModel).Assembly);
      }
      else
      {
        IsPlaying = false;
        ButtonText = "Sound off";
        Preferences.Set("playMusic", false);
        await CrossMediaManager.Current.Stop();
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
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }
  }
}
