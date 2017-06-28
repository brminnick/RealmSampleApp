using System.Threading;

using NUnit.Framework;

using Xamarin.UITest;

namespace RealmSampleApp.UITests
{
    public class Tests : BaseTest
    {
        #region Constructors
        public Tests(Platform platform) : base(platform)
        {
        }
        #endregion

        #region Methods
        [Test]
        public void AppLaunches()
        {

        }

        [TestCase("Brandon", "Minnick", "123-456-7890", true)]
        [TestCase("Brandon", "Minnick", "123-456-7890", false)]
        public void AddContactTest(string firstName, string lastName, string phoneNumber, bool shouldUseReturnKey)
        {
            ContactsListPage.TapAddContactButton();

            App.WaitForElement(ContactDetailsPage.Title);

            ContactDetailsPage.PopulateAllTextFields(firstName, lastName, phoneNumber, shouldUseReturnKey);

            switch(shouldUseReturnKey)
            {
                case false:
                    ContactDetailsPage.TapSaveButton();
                    break;
            }

            while (ContactsListPage.IsRefreshActivityIndicatorDisplayed)
                Thread.Sleep(100);

            Assert.IsTrue(ContactsListPage.DoesViewCellExist($"{firstName} {lastName}"));
            Assert.IsTrue(ContactsListPage.DoesViewCellExist(phoneNumber));
        }

        protected override void BeforeEachTest()
        {
            base.BeforeEachTest();

            App.WaitForElement(ContactsListPage.Title);
        }
        #endregion
    }
}
