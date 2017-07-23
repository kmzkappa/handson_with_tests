using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using System.Threading.Tasks;

namespace Android.UITest
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                // TODO: Update this path to point to your Android app and uncomment the
                // code if the app is not included in the solution.
                //.ApkFile ("../../../Android/bin/Debug/UITestsAndroid.apk")
                .InstalledApp("Mobile.SuperStreamNX")
                .EnableLocalScreenshots()
                .StartApp();
        }

        //[Test]
        //public void AppLaunches()
        //{
        //    //app.Screenshot("First screen.");
        //    //app.Repl();
        //}

        //[Test]
        //public void AnyTest()
        //{
        //    app.Flash(c => c.TextField().Index(1));
        //}


        //[Test]
        //public async Task Login_Error()
        //{
        //    app.EnterText(c => c.TextField().Index(1), "DummyID");
        //    app.EnterText(c => c.TextField().Index(2), "DummyPassword");
        //    app.Tap(c => c.Button().Index(0));
        //    await Task.Delay(1000);
        //    app.WaitForElement(c => c.Class("FormsTextView").Text("ログイン情報に誤りがあります。ログインできません。（Login information is/are incorrect.）"));
        //}


        [Test]
        public async Task Login_Error()
        {
            app.EnterText(c => c.TextField().Index(1), "DummyID");
            app.EnterText(c => c.TextField().Index(2), "DummyPassword");
            app.Tap(c => c.Button().Index(0));
            await Task.Delay(1000);
            app.WaitForElement(c => c.Class("FormsTextView"));
        }


        [Test]
        public async Task Login_Error2()
        {
            app.EnterText(c => c.TextField().Index(1), "DummyID");
            app.EnterText(c => c.TextField().Index(2), "DummyPassword");
            app.Tap(c => c.Button().Index(0));
            await Task.Delay(1000);
            app.WaitForNoElement(c => c.Class("FormsTextView"));
        }
    }
}

