using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class GamerTagViewModel : INotifyPropertyChanged
    {
        public string _gamerTag;
        public ICommand ButtonCommand { get; set; }
        public string GamerTag {
            get
            {
                return _gamerTag;
            }
            set
            {
                _gamerTag = value;
                if(PropertyChanged != null)                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(GamerTag)));
                }
            }
        }

        public GamerTagViewModel()
        {
            GamerTag = Preferences.Get("NameTag", string.Empty);
            ButtonCommand = new Command(ButtonCommandExecute);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ButtonCommandExecute(object obj)
        {
            Preferences.Set("NameTag", GamerTag);
        }
    }
}
