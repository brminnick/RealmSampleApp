using System;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace RealmSampleApp.UITests
{
    public static class AppInitializer
    {
        public static IApp StartApp(Platform platform) => platform switch
        {
            Platform.Android => ConfigureApp.Android.StartApp(AppDataMode.Clear),
            Platform.iOS => ConfigureApp.iOS.StartApp(AppDataMode.Clear),
            _ => throw new NotSupportedException()
        };
    }
}
