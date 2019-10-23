using System;
using System.ComponentModel;
using PrettyWeather.Tizen;
using PrettyWeather.Tizen.Interop;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(PancakeView), typeof(PancakeViewRenderer))]
namespace PrettyWeather.Tizen
{
    class PancakeViewRenderer : ViewRenderer<PancakeView, ElmSharp.Box>
    {
        private RoundRectangle _roundRectangle;
        private BorderRectangle _borderRectangle;
        private IntPtr _evasMap;

        public PancakeViewRenderer() : base()
        {
        }

        /// <summary>
        /// This method ensures that we don't get stripped out by the linker.
        /// </summary>
        public static new void Init()
        {
#pragma warning disable 0219
            var ignore1 = typeof(PancakeViewRenderer);
            var ignore2 = typeof(PancakeView);
#pragma warning restore 0219
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PancakeView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new ElmSharp.Box(Forms.NativeParent));
                Control.SetLayoutCallback(OnLayout);
                _roundRectangle = new RoundRectangle(Forms.NativeParent);
                _roundRectangle.Show();
                _borderRectangle = new BorderRectangle(Forms.NativeParent);
                _borderRectangle.Show();
                Control.PackEnd(_roundRectangle);
                Control.PackEnd(_borderRectangle);
            }

            var pancake = (Element as PancakeView);

            UpdateCornerRadius(pancake);
            UpdateBorder(pancake);
            PackChild();
            base.OnElementChanged(e);
        }

        protected override void UpdateBackgroundColor(bool initialize)
        {
            if (Element.BackgroundColor != default(Xamarin.Forms.Color))
            {
                _roundRectangle.Color = Element.BackgroundColor.ToNative();
            }
            var pancake = Element as PancakeView;
            if ((pancake.BackgroundGradientStartColor != default(Xamarin.Forms.Color) && pancake.BackgroundGradientEndColor != default(Xamarin.Forms.Color)))
            {
                _evasMap = LocalInterop.evas_map_new(4);
                LocalInterop.evas_map_util_points_populate_from_object_full(_evasMap, _roundRectangle.Handle, 0);
                ElmSharp.Color startColor = pancake.BackgroundGradientStartColor.ToNative();
                ElmSharp.Color endColor = pancake.BackgroundGradientEndColor.ToNative();
                LocalInterop.evas_map_point_color_set(_evasMap, 0, startColor.R, startColor.G, startColor.B, startColor.A);
                LocalInterop.evas_map_point_color_set(_evasMap, 1, startColor.R, startColor.G, startColor.B, startColor.A);
                LocalInterop.evas_map_point_color_set(_evasMap, 2, endColor.R, endColor.G, endColor.B, endColor.A);
                LocalInterop.evas_map_point_color_set(_evasMap, 3, endColor.R, endColor.G, endColor.B, endColor.A);
                //LocalInterop.evas_map_util_points_color_set(_evasMap, 255, 0, 0, 255);
                LocalInterop.evas_object_map_set(_roundRectangle.Handle, _evasMap);
                LocalInterop.evas_object_map_enable_set(_roundRectangle.Handle, true);
            }
        }

        private void PackChild()
        {
            if (Element.Content == null)
                return;
            IVisualElementRenderer renderer = Xamarin.Forms.Platform.Tizen.Platform.GetOrCreateRenderer(Element.Content);
            Control.PackEnd(renderer.NativeView);
        }

        private void OnLayout()
        {
            _roundRectangle.Draw(Control.Geometry);
            _borderRectangle.Draw(Control.Geometry);
            UpdateBackgroundColor(false);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var pancake = (Element as PancakeView);

            if (e.PropertyName == PancakeView.ContentProperty.PropertyName)
            {
                PackChild();
            }
            else if (e.PropertyName == PancakeView.CornerRadiusProperty.PropertyName)
            {
                UpdateCornerRadius(pancake);
            }
            else if (e.PropertyName == PancakeView.BackgroundGradientStartColorProperty.PropertyName ||
                e.PropertyName == PancakeView.BackgroundGradientEndColorProperty.PropertyName ||
                e.PropertyName == PancakeView.BackgroundGradientStopsProperty.PropertyName)
            {
                UpdateBackgroundColor(false);
            }
            else if (e.PropertyName == PancakeView.BorderColorProperty.PropertyName ||
                e.PropertyName == PancakeView.BorderThicknessProperty.PropertyName)
            {
                UpdateBorder(pancake);
            }
        }

        private void UpdateCornerRadius(PancakeView pancake)
        {
            pancake.CornerRadius.Deconstruct(out double topLeft, out double topRight, out double bottomLeft, out double bottomRight);
            _borderRectangle.SetRadius(Forms.ConvertToScaledPixel(topLeft), Forms.ConvertToScaledPixel(topRight),
                Forms.ConvertToScaledPixel(bottomLeft), Forms.ConvertToScaledPixel(bottomRight));
            _roundRectangle.SetRadius(Forms.ConvertToScaledPixel(topLeft), Forms.ConvertToScaledPixel(topRight),
                Forms.ConvertToScaledPixel(bottomLeft), Forms.ConvertToScaledPixel(bottomRight));
            _roundRectangle.Draw();
            _borderRectangle.Draw();
        }

        private void UpdateBorder(PancakeView pancake)
        {
            _borderRectangle.Color = pancake.BorderColor.ToNative();
            _borderRectangle.BorderWidth = Forms.ConvertToScaledPixel(pancake.BorderThickness);
            _borderRectangle.Draw();
        }
    }
}
