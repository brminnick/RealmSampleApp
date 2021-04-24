using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RealmSampleApp.Constants;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace RealmSampleApp.UITests
{
    class ContactsListPage : BasePage
    {
        readonly Query _addContactButon;

        public ContactsListPage(IApp app) : base(app, PageTitles.ContactsListPage)
        {
            _addContactButon = x => x.Marked(AutomationIdConstants.AddContactButon);
        }

        public bool IsRefreshActivityIndicatorDisplayed =>
            GetIsRefreshActivityIndicatorDisplayed();

        public void TapAddContactButton()
        {
            if (App is iOSApp)
                App.Tap(_addContactButon);
            else if (App is AndroidApp)
                App.Tap(x => x.Class("ActionMenuItemView"));
            else
                throw new NotSupportedException();

            App.Screenshot("Tapped Add Contact Button");
        }

        public bool DoesViewCellExist(string fullName)
        {
            try
            {
                App.ScrollDownTo(fullName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task WaitForNoPullToRefreshActivityIndicatorAsync(TimeSpan? timeout = null)
        {
            timeout ??= TimeSpan.FromSeconds(10);

            int loopCount = 0;

            while (IsRefreshActivityIndicatorDisplayed)
            {
                if (loopCount / 10 > timeout.Value.TotalSeconds)
                    Assert.Fail("WaitForNoPullToRefreshActivityIndicatorAsync Failed");

                loopCount++;
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            }
        }

        public async Task WaitForPullToRefreshActivityIndicatorAsync(TimeSpan? timeout = null)
        {
            timeout ??= TimeSpan.FromSeconds(10);

            int loopCount = 0;

            while (!IsRefreshActivityIndicatorDisplayed)
            {
                if (loopCount / 10 > timeout.Value.TotalSeconds)
                    Assert.Fail("WaitForPullToRefreshActivityIndicatorAsync Failed");

                loopCount++;
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            }
        }

        bool GetIsRefreshActivityIndicatorDisplayed() => App switch
        {
            iOSApp => App.Query(x => x.Class("UIRefreshControl")).Any(),
            AndroidApp => (bool)App.Query(x => x.Class("SwipeRefreshLayout").Invoke("isRefreshing")).First(),
            _ => throw new NotSupportedException()
        };
    }
}
