﻿using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;

namespace RealmSampleApp.UITests
{
    class Tests : BaseTest
    {
        public Tests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void AppLaunches()
        {

        }

        [TestCase("Brandon", "Minnick", "123-456-7890", true)]
        [TestCase("Brandon", "Minnick", "123-456-7890", false)]
        public async Task AddContactTest(string firstName, string lastName, string phoneNumber, bool shouldUseReturnKey)
        {
            ContactsListPage.TapAddContactButton();

            App.WaitForElement(ContactDetailsPage.Title);

            ContactDetailsPage.PopulateAllTextFields(firstName, lastName, phoneNumber, shouldUseReturnKey);

            if (!shouldUseReturnKey)
                ContactDetailsPage.TapSaveButton();

            await ContactsListPage.WaitForPullToRefreshActivityIndicatorAsync();
            await ContactsListPage.WaitForNoPullToRefreshActivityIndicatorAsync();

            Assert.IsTrue(ContactsListPage.DoesViewCellExist($"{firstName} {lastName}"));
            Assert.IsTrue(ContactsListPage.DoesViewCellExist(phoneNumber));
        }

        [TestCase("Brandon", "Minnick", "123-456-7890")]
        public void EnterContactInformationThenPressCancel(string firstName, string lastName, string phoneNumber)
        {
            ContactsListPage.TapAddContactButton();

            App.WaitForElement(ContactDetailsPage.Title);

            ContactDetailsPage.PopulateAllTextFields(firstName, lastName, phoneNumber, false);
            ContactDetailsPage.TapCancelButton();

            Assert.IsFalse(ContactsListPage.DoesViewCellExist($"{firstName} {lastName}"));
        }

        protected override void BeforeEachTest()
        {
            base.BeforeEachTest();

            App.Screenshot("App Launched");
            App.WaitForElement(ContactsListPage.Title);
        }
    }
}
