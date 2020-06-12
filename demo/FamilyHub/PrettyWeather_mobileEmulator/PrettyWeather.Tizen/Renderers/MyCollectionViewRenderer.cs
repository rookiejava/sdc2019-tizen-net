using System;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

namespace PrettyWeather.Tizen
{
    public class MyCollectionView : Xamarin.Forms.Platform.Tizen.Native.CollectionView, ICollectionViewController
    {

        public MyCollectionView(EvasObject parent) : base(parent)
        {
        }

        protected override ElmSharp.Scroller CreateScroller(EvasObject parent)
        {
            var _scroller = base.CreateScroller(parent);
            _scroller.HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;

            return _scroller;
        }
    }

    class MyCollectionViewRenderer : ItemsViewRenderer
    {
        public MyCollectionViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StructuredItemsView> e)
        {
            SetNativeControl(new MyCollectionView(Forms.NativeParent));
            base.OnElementChanged(e);
        }
    }
}
