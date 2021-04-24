using Xamarin.Forms;

namespace RealmSampleApp
{
    class BaseNavigationPage : NavigationPage
    {
        public BaseNavigationPage(ContentPage root) : base(root)
        {
            BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor;
            BarTextColor = ColorConstants.NavigationBarTextColor;
        }
    }
}
