using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NumberBomb.Droid.Services;
using NumberBomb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
[assembly: Dependency(typeof(MediaService))]
namespace NumberBomb.Droid.Services
{
  public class MediaService : IMediaService
  {
    public void StarMusic()
    {
      MessagingCenter.Send<MediaService>(this, "start");
    }

    public void StopMusic()
    {
      MessagingCenter.Send<MediaService>(this, "stop");
    }
  }
}