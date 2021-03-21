﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
    public class ChangeUserViewModel : INotifyPropertyChanged
    {
        public string _newName;
        public ICommand SaveButtonCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string ChangeName
        {
            get
            {
                return _newName;
            }
            set
            {
                _newName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ChangeName)));
                }
            }
        }

        public ChangeUserViewModel()
        {
            ChangeName = Preferences.Get("NameTag", string.Empty);
            SaveButtonCommand = new Command(SaveCommandExecute);
        }

        private void SaveCommandExecute(object obj)
        {
            if (!String.IsNullOrEmpty(ChangeName))
            {
                Preferences.Set("NameTag", ChangeName);
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}