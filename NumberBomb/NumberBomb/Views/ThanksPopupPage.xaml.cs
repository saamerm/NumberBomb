﻿using NumberBomb.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NumberBomb
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ThanksPopupPage : PopupPage
  {
    public ThanksPopupPage()
    {
      InitializeComponent();
      BindingContext = new ThanksPopupViewModel();
    }
  }
}