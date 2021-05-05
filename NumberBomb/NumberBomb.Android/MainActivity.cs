using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaManager;
using AndroidX.AppCompat.App;
using NumberBomb.Droid.Services;
using Xamarin.Forms;
using NumberBomb.ViewModels;

namespace NumberBomb.Droid
{
    [Activity(Label = "NumberBomb", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            CrossMediaManager.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      try
      {
        var audio = CrossMediaManager.Current;
        MessagingCenter.Subscribe<MediaService>(this, "start",async (sender) =>
        {
          await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
        });
        MessagingCenter.Subscribe<MediaService>(this, "stop",async (sender) =>
        {
          await audio.Stop();
        });

      }
      catch (Exception ex)
      {
      }
      LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}