using NUnit.Framework;
using Xamarin.UITest;

namespace RealmSampleApp.UITests
{
    class ReplTests : BaseTest
    {
        public ReplTests(Platform platform) : base(platform)
        {
        }

        [Ignore("Only for testing")]
        [Test]
        public void Repl() => App.Repl();
    }
}
