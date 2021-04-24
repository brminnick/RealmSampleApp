using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace RealmSampleApp.UITests
{
    [TestFixture(Platform.iOS)]
    [TestFixture(Platform.Android)]
    abstract class BaseTest
    {
        IApp? _app;
        ContactsListPage? _contactsListPage;
        ContactDetailsPage? _contactsDetailsPage;

        protected BaseTest(Platform platform) => Platform = platform;

        protected IApp App => _app ?? throw new NullReferenceException();
        protected ContactsListPage ContactsListPage => _contactsListPage ?? throw new NullReferenceException();
        protected ContactDetailsPage ContactDetailsPage => _contactsDetailsPage ?? throw new NullReferenceException();

        protected Platform Platform { get; }

        [SetUp]
        protected virtual void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(Platform);

            _contactsListPage = new ContactsListPage(App);
            _contactsDetailsPage = new ContactDetailsPage(App);
        }
    }
}

