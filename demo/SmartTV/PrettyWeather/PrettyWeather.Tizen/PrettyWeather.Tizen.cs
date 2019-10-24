using System;
using Xamarin.Forms;
using PrettyWeather.Tizen;
using Tizen.Appium;

[assembly: ExportRenderer(typeof(PrettyWeather.MyCollectionView), typeof(MyCollectionViewRenderer))]
[assembly: ExportRenderer(typeof(PrettyWeather.ViewHolder), typeof(ViewHolderRenderer))]
namespace PrettyWeather
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            ElmSharp.Elementary.FocusAutoScrollMode = ElmSharp.FocusAutoScrollMode.Show;
            TizenAppium.StartService();
            app.Run(args);
        }
    }
}
