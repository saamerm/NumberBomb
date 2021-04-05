using Newtonsoft.Json;
using NumberBomb.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.ViewModels
{
  public class FeedBackViewModel : INotifyPropertyChanged
  {
    private bool _isBusy;
    public event PropertyChangedEventHandler PropertyChanged;
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
    public ICommand SubmitCommand { get; set; }
    public ICommand BackCommand { get; set; }
    private string _feedBackAdded;
    public string FeedbackAdded
    {
      get
      {
        return _feedBackAdded;
      }
      set
      {
        _feedBackAdded = value;
        if (PropertyChanged != null)
        {
          PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FeedbackAdded)));
        }
      }
    }
    public AddFeedbackRequestModel AddFeedback { get; set; }
    public FeedBackViewModel()
    {
      AddFeedback = new AddFeedbackRequestModel();
      BackCommand = new Command(BackCommandExcute);
      SubmitCommand = new Command(SubmitCommandExcute);
      if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
      {
        PopupNavigation.Instance.PopAsync();
      }
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();
    }
    private async void SubmitCommandExcute(object obj)
    {
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var dialog = new ThanksPopupPage();
          dialog.BindingContext = this;
          if (!string.IsNullOrEmpty(AddFeedback.Name) && !string.IsNullOrEmpty(AddFeedback.Email) && !string.IsNullOrEmpty(AddFeedback.Feedback))
          {

            var result = await AddFeedBack();

            if (result.Status == "Success")
            {
              FeedbackAdded = result.Message;
              await PopupNavigation.Instance.PushAsync(dialog);
              Device.StartTimer(new TimeSpan(0, 0, 5), () =>
              {
                App.Current.MainPage.Navigation.PushAsync(new HomePage());
                return false;
              });
            }
            else
            {
              FeedbackAdded = "Something wrong happen try agian later";
              await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
          }
          else
          {
            FeedbackAdded = "Please Enter all the field";
            await PopupNavigation.Instance.PushAsync(dialog);
          }
        }
      }
      catch (Exception e)
      {
        IsBusy = false;
       // Crashes.TrackError(e);
      }
      finally
      {
        IsBusy = false;
      }
    }
    private async Task<AddFeedbackResponseModel> AddFeedBack()
    {
      var client = new HttpClient();
      var data = new AddFeedbackRequestModel { Name = AddFeedback.Name, Email = AddFeedback.Email, Feedback = AddFeedback.Feedback };
      var jsonString = JsonConvert.SerializeObject(data);
      var requestContent = new StringContent(jsonString);
      var response = client.PostAsync("https://script.google.com/macros/s/AKfycbydFc7hHxvt_yiuT2qHYk6cu0aCLjB_RV-AxrPRQlglhvbMktHtPenfOuDw_PH2XB9z/exec", requestContent).Result;
      System.Console.WriteLine(response.StatusCode);
      var result = response.Content.ReadAsStringAsync().Result;
      System.Console.WriteLine(result);
      var feedbackAdded = JsonConvert.DeserializeObject<AddFeedbackResponseModel>(result);
      return feedbackAdded;
    }
  }
}
