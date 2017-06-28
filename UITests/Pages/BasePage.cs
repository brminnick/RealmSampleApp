using System;
using System.Linq;

using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RealmSampleApp.UITests
{
    public abstract class BasePage
    {
        #region Constant Fields
        readonly string _pageTitle;
        #endregion

        #region Constructors
        protected BasePage(IApp app, Platform platform, string pageTitle)
        {
            App = app;

            OnAndroid = platform == Platform.Android;
            OniOS = platform == Platform.iOS;

            _pageTitle = pageTitle;
        }
        #endregion

        #region Methods
		protected string Title => GetTitle();
        protected IApp App { get; }
        protected bool OnAndroid { get; }
        protected bool OniOS { get; }
        #endregion

        #region Methods
        string GetTitle(int timeoutInSeconds = 60)
        {
            App.WaitForElement(_pageTitle, "Could Not Retrieve Page Title", TimeSpan.FromSeconds(timeoutInSeconds));

            AppResult[] titleQuery;
            if (OniOS)
                titleQuery = App.Query(x => x.Class("UILabel").Marked(_pageTitle));
            else
                titleQuery = App.Query(x => x.Class("AppCompatTextView").Marked(_pageTitle));

            return titleQuery?.FirstOrDefault()?.Text;
        }
        #endregion
    }
}

