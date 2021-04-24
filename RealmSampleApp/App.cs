using Xamarin.Forms;

namespace RealmSampleApp
{
    public class App : Application
    {
        public App() => MainPage = new BaseNavigationPage(new ContactsListPage());
    }
}
