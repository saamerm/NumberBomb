using MediaManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
  public class InfoViewModel: BaseViewModel, INotifyPropertyChanged
    {
    private string _playImage;
    public string PlayImage
    {
      get
      {
        return _playImage;
      }
      set
      {
        _playImage = value;
        if (PropertyChanged != null)
        {
          PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayImage)));
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
    public ICommand BackButtonClicked { get; set; }

    public ICommand PlayCommand { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    public InfoViewModel()
    {
      PlayImage = "volume_up_24px.png";
      PlayCommand = new Command(PlayCommandExcute);
      BackButtonClicked = new Command(BackButtonClickedCommandExecute);
    }

    private void BackButtonClickedCommandExecute(object obj)
    {
      Application.Current.MainPage.Navigation.PopAsync();
    }

    private async void PlayCommandExcute(object obj)
    {
      if (!IsPlaying)
      {
       
        PlayImage = "volume_up_24px.png";
        IsPlaying = true;
        var audio = CrossMediaManager.Current;
        await audio.PlayFromAssembly("numberbombmusic.mp3", typeof(InfoViewModel).Assembly);
      }
      else
      {
        PlayImage = "volume_off_24px.png";
        IsPlaying = false;
      
        await CrossMediaManager.Current.Stop();
      }
    }
  }
}
