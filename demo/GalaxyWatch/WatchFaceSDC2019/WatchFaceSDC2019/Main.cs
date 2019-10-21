using System;
using Xamarin.Forms;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;

namespace WatchFaceSDC2019
{
    class Program : FormsWatchface
    {
        WatchViewModel _viewModel;

        protected override void OnCreate()
        {
            base.OnCreate();
            ElmSharp.Utility.AppendGlobalFontPath(DirectoryInfo.Resource);
            _viewModel = new WatchViewModel();
            var watchfaceApp = new App
            {
                BindingContext = _viewModel
            };
            DateTime currentTime = DateTime.Now;
            _viewModel.Date = currentTime.ToString("MMM dd").ToUpper();
            _viewModel.Time = currentTime.ToString("HH:mm");
            SetTimeTickFrequency(1, TimeTickResolution.TimeTicksPerMinute);
            LoadWatchface(watchfaceApp);
        }

        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null)
            {
                DateTime currentTime = time.Time.UtcTimestamp;
                _viewModel.Date = currentTime.ToString("MMM dd").ToUpper();
                _viewModel.Time = currentTime.ToString("HH:mm");
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
