using System;
using NumberBomb.Helper;
using Microsoft.AppCenter.Analytics;

namespace NumberBomb.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Analytics.TrackEvent("PageView: " + this.GetType().Name.Replace("ViewModel", string.Empty).SplitPascalCase());
        }
    }
}
