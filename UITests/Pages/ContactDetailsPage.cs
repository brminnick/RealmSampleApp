using System;
using System.Linq;

using Xamarin.UITest;
using RealmSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace RealmSampleApp.UITests
{
    public class ContactDetailsPage : BasePage
    {
        #region Constant Fields
        readonly Query _firstNameEntry, _lastNameEntry, _phoneNumberEntry, _saveButton, _cancelButton;
        #endregion

        #region Constructors
        public ContactDetailsPage(IApp app, Platform platform) : base(app, platform, PageTitles.ContactDetailsPage)
        {
            _firstNameEntry = x => x.Marked(AutomationIdConstants.FirstNameEntry);
            _lastNameEntry = x => x.Marked(AutomationIdConstants.LastNameEntry);
            _phoneNumberEntry = x => x.Marked(AutomationIdConstants.PhoneNumberEntry);
            _saveButton = x => x.Marked(AutomationIdConstants.SaveContactButton);
			_cancelButton = x => x.Marked(AutomationIdConstants.CancelContactButton);
        }
        #endregion

        #region Properties
        public bool IsRefreshActivityIndicatorDisplayed
        {
            get
            {
                if (OnAndroid)
                    return (bool)App.Query(x => x.Class("SwipeRefreshLayout").Invoke("isRefreshing")).FirstOrDefault();

                if (OniOS)
                    return App.Query(x => x.Class("UIRefreshControl")).Any();

                throw new Exception("Platform Not Recognized");
            }
        }
        #endregion
    }
}
