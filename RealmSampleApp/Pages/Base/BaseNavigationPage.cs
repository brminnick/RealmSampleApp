using Xamarin.Forms;

namespace RealmSampleApp
{
    public class BaseNavigationPage : NavigationPage
    {
        public BaseNavigationPage(ContentPage root) : base(root)
        {
            BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor;
            BarTextColor = ColorConstants.NavigationBarTextColor;
        }
    }
}
