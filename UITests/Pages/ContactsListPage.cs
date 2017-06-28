using System.Linq;

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
            App.Tap(_addContactButon);
            App.Screenshot("Tapped Add Contact Button");
        }

        public bool DoesViewCellExist(string fullName)
        {
            try
            {
                App.ScrollDownTo(fullName);
                return true;
            }
            catch(System.Exception e)
            {
                return false;
            }
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
