using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace PrettyWeather.Tizen
{
    class ViewHolderRenderer : ButtonRenderer
    {
        public ViewHolderRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Color = ElmSharp.Color.Transparent;

            }
        }
    }
}
