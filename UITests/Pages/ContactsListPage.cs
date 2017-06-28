﻿using System.Linq;
using System.Threading.Tasks;

using Xamarin.UITest;

using RealmSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace RealmSampleApp.UITests
{
    public class ContactsListPage : BasePage
    {
        #region Constant Fields
        readonly Query _addContactButon;
        #endregion

        #region Constructors
        public ContactsListPage(IApp app, Platform platform) : base(app, platform, PageTitles.ContactsListPage)
        {
            _addContactButon = x => x.Marked(AutomationIdConstants.AddContactButon);
        }
        #endregion

        #region Properties
        public bool IsRefreshActivityIndicatorDisplayed =>
            GetIsRefreshActivityIndicatorDisplayed();
        #endregion

        #region Methods
        public void TapAddContactButton()
        {
            switch (OniOS)
            {
                case true:
                    App.Tap(_addContactButon);
                    break;
                default:
                    App.Tap(x => x.Class("ActionMenuItemView"));
                    break;
            }

            App.Screenshot("Tapped Add Contact Button");
        }

        public bool DoesViewCellExist(string fullName)
        {
            try
            {
                App.ScrollDownTo(fullName);
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

		public async Task WaitForNoPullToRefreshActivityIndicatorAsync()
		{
            while (IsRefreshActivityIndicatorDisplayed)
                await Task.Delay(10);
		}

		public async Task WaitForPullToRefreshActivityIndicatorAsync()
		{
			while (!IsRefreshActivityIndicatorDisplayed)
				await Task.Delay(10);
		}

        bool GetIsRefreshActivityIndicatorDisplayed()
        {
            switch (OniOS)
            {
                case true:
                    return App.Query(x => x.Class("UIRefreshControl")).Any();

                default:
                    return (bool)App.Query(x => x.Class("SwipeRefreshLayout").Invoke("isRefreshing")).FirstOrDefault();
            }
        }
        #endregion
    }
}
