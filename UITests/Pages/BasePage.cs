using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace RealmSampleApp.UITests
{
    public abstract class BasePage
    {
        readonly string _pageTitleText;

        protected BasePage(IApp app, string pageTitle)
        {
            App = app;
            _pageTitleText = pageTitle;
        }

        public string Title => GetTitle();

        protected IApp App { get; }

        string GetTitle(TimeSpan? timeout = null)
        {
            App.WaitForElement(_pageTitleText, "Could Not Retrieve Page Title", timeout);

            AppResult[] titleQuery = App switch
            {
                iOSApp => App.Query(x => x.Class("UILabel").Marked(_pageTitleText)),
                AndroidApp => App.Query(x => x.Class("AppCompatTextView").Marked(_pageTitleText)),
                _ => throw new NotSupportedException()
            };

            return titleQuery.First().Text;
        }
    }
}

