using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace RealmSampleApp.UITests
{
    public static class AppInitializer
	{
		public static IApp StartApp(Platform platform)
		{
			if (platform == Platform.Android)
			{
				return ConfigureApp.Android.StartApp(AppDataMode.Clear);
			}

			return ConfigureApp.iOS.StartApp(AppDataMode.Clear);
		}
	}
}
