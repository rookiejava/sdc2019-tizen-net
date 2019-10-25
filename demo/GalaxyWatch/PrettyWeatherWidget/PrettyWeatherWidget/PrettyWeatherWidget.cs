using System;
using Xamarin.Forms;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Tizen.Wearable.CircularUI.Forms.Renderer.Widget;

namespace PrettyWeather
{
    class PrettyWeatherWidgetBase : FormsWidgetBase
    {
        public override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            var app = new App();
            LoadApplication(app);
        }
    }

    class PrettyWeatherWidgetApp : FormsWidgetApplication
    {
        public PrettyWeatherWidgetApp(Type type) : base(type)
        {
        }

        static void Main(string[] args)
        {
            var app = new PrettyWeatherWidgetApp(typeof(PrettyWeatherWidgetBase));
            Forms.Init(app);
            FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
