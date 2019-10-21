using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchFaceSDC2019
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchView : ContentView
    {
        Animation _symbolAnimation;
        bool _isHero1AnimaitionRunning = false;
        bool _isHero2AnimaitionRunning = false;
        bool _isHero3AnimaitionRunning = false;

        public WatchView()
        {
            InitializeComponent();
        }

        private async void OnHero1Clicked(object sender, EventArgs args)
        {
            if(_isHero1AnimaitionRunning)
                return;

            _isHero1AnimaitionRunning = true;
            MakeAndCommitSymbolAnimation();
            string hero1 = ((WatchViewModel)BindingContext).Hero1ImageSource;
            var x = HeroImage1.Bounds.X;
            await Task.WhenAll(new List<Task> { HeroImage1.LayoutTo(new Rectangle(-200, HeroImage1.Bounds.Y, HeroImage1.Bounds.Width, HeroImage1.Bounds.Height), 500, Easing.SinIn) });
            ((WatchViewModel)BindingContext).Hero1ImageSource = hero1.Equals(WatchViewModel.s_defaultHero1ImageSource) ? WatchViewModel.s_altHero1ImageSource : WatchViewModel.s_defaultHero1ImageSource;
            await Task.WhenAll(new List<Task> { HeroImage1.LayoutTo(new Rectangle(x, HeroImage1.Bounds.Y, HeroImage1.Bounds.Width, HeroImage1.Bounds.Height), 500, Easing.SinOut) });
            _isHero1AnimaitionRunning = false;
        }

        private async void OnHero2Clicked(object sender, EventArgs args)
        {
            if (_isHero2AnimaitionRunning)
                return;

            _isHero2AnimaitionRunning = true;
            CommitShakeAnimation();
            uint timeout = 500;
            if (HeroImage2.IsVisible)
            {
                HeroImage2Alt.RotationY = -270;
                await HeroImage2.RotateYTo(-90, timeout, Easing.SpringIn);
                HeroImage2.IsVisible = false;
                HeroImage2Alt.IsVisible = true;
                await HeroImage2Alt.RotateYTo(-360, timeout, Easing.SpringOut);
                HeroImage2Alt.RotationY = 0;
            }
            else
            {
                HeroImage2.RotationY = -270;
                await HeroImage2Alt.RotateYTo(-90, timeout, Easing.SpringIn);
                HeroImage2Alt.IsVisible = false;
                HeroImage2.IsVisible = true;
                await HeroImage2.RotateYTo(-360, timeout, Easing.SpringOut);
                HeroImage2.RotationY = 0;
            }
            CommitShakeAnimation();
            _isHero2AnimaitionRunning = false;
        }

        private async void OnHero3Clicked(object sender, EventArgs args)
        {
            if (_isHero3AnimaitionRunning)
                return;

            _isHero3AnimaitionRunning = true;
            await BGColorChangeTask(Color.Black, Color.White);
            await BGColorChangeTask(Color.White, Color.Black);
            await BGColorChangeTask(Color.Black, Color.White);
            await BGColorChangeTask(Color.White, Color.Black);

            string hero3 = ((WatchViewModel)BindingContext).Hero3ImageSource;
            if (hero3.Equals(WatchViewModel.s_defaultHero3ImageSource))
            {
                await Task.WhenAll(new List<Task> { Hero3Eyes.ScaleTo(3, 250, Easing.SpringIn), BGColorChangeTask(Color.Black, Color.White) });
                await Task.WhenAll(new List<Task> { Hero3Eyes.ScaleTo(1, 250, Easing.SpringOut), BGColorChangeTask(Color.White, Color.Black) });
                await Task.WhenAll(new List<Task> { HeroImage3.FadeTo(0, 100, Easing.CubicOut), Hero3Eyes.FadeTo(0, 100, Easing.CubicOut), BGColorChangeTask(Color.Black, Color.White) });
                ((WatchViewModel)BindingContext).Hero3ImageSource = WatchViewModel.s_altHero3ImageSource;
                await Task.WhenAll(new List<Task> { HeroImage3.FadeTo(1, 100, Easing.CubicIn), BGColorChangeTask(Color.White, Color.Black) });
            }
            else
            {
                await Task.WhenAll(new List<Task> { HeroImage3.FadeTo(0, 100, Easing.CubicOut), Hero3Eyes.FadeTo(0, 100, Easing.CubicOut), BGColorChangeTask(Color.Black, Color.White) });
                ((WatchViewModel)BindingContext).Hero3ImageSource = WatchViewModel.s_defaultHero3ImageSource;
                await Task.WhenAll(new List<Task> { HeroImage3.FadeTo(1, 100, Easing.CubicIn), Hero3Eyes.FadeTo(1, 100, Easing.CubicIn), BGColorChangeTask(Color.White, Color.Black) });
            }

            await BGColorChangeTask(Color.Black, Color.White);
            await BGColorChangeTask(Color.White, Color.Black);
            BGSymbolImage.BackgroundColor = Color.Transparent;
            _isHero3AnimaitionRunning = false;
        }

        private void MakeAndCommitSymbolAnimation()
        {
            _symbolAnimation = new Animation();
            _symbolAnimation.Add(0, 0.2, new Animation(v => BGSymbolImage.Opacity = v, 1, 0, Easing.SinOut));
            _symbolAnimation.Add(0.8, 1, new Animation(v => BGSymbolImage.Opacity = v, 0, 1, Easing.SinIn));
            AddSymbolAnimation(Symbol1, 0, 0.8);
            AddSymbolAnimation(Symbol2, 0.1, 0.9);
            AddSymbolAnimation(Symbol3, 0.3, 1);
            AddSymbolAnimation(Symbol4, 0.25, 0.95);
            AddSymbolAnimation(Symbol5, 0.15, 0.85);
            AddSymbolAnimation(Symbol6, 0.4, 1);
            AddSymbolAnimation(Symbol7, 0.2, 1);
            AddSymbolAnimation(Symbol8, 0.15, 0.95);
            AddSymbolAnimation(Symbol9, 0.4, 1);
            AddSymbolAnimation(Symbol1, 0.4, 1);
            AddSymbolAnimation(Symbol2, 0.5, 1);
            AddSymbolAnimation(Symbol3, 0.8, 1);
            AddSymbolAnimation(Symbol4, 0.65, 1);
            AddSymbolAnimation(Symbol5, 0.55, 1);
            AddSymbolAnimation(Symbol6, 0.9, 1);
            AddSymbolAnimation(Symbol7, 0.6, 1);
            AddSymbolAnimation(Symbol8, 0.55, 1);
            AddSymbolAnimation(Symbol9, 0.8, 1);
            _symbolAnimation.Commit(this, "MultiAnimation", 16, 3000, null);
        }

        private void AddSymbolAnimation(Image symbol, double beginAt, double finishAt)
        {
            var fadeInAnimation = new Animation(v => symbol.Opacity = v, 0, 1, Easing.SinIn);
            var moveAnimation = new Animation(v => symbol.TranslationY = v, Symbol1.Y, Symbol1.Y + 320, Easing.Linear);
            var fadeOutAnimation = new Animation(v => symbol.Opacity = v, 1, 0, Easing.SinOut);
            _symbolAnimation.Add(0, 0.3, fadeInAnimation);
            _symbolAnimation.Add(beginAt, finishAt, moveAnimation);
            _symbolAnimation.Add(0.7, 1, fadeOutAnimation);
        }

        private async void CommitShakeAnimation()
        {
            uint timeout = 50;
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(-15, 0, timeout), HeroImage1.TranslateTo(-15, 0, timeout), LabelDate.TranslateTo(-15, 0, timeout), LabelTime.TranslateTo(-15, 0, timeout), HeroImage3.TranslateTo(-15, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(15, 0, timeout), HeroImage1.TranslateTo(15, 0, timeout), LabelDate.TranslateTo(15, 0, timeout), LabelTime.TranslateTo(15, 0, timeout), HeroImage3.TranslateTo(15, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(-9, 0, timeout), HeroImage1.TranslateTo(-9, 0, timeout), LabelDate.TranslateTo(-9, 0, timeout), LabelTime.TranslateTo(-9, 0, timeout), HeroImage3.TranslateTo(-9, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(9, 0, timeout), HeroImage1.TranslateTo(9, 0, timeout), LabelDate.TranslateTo(9, 0, timeout), LabelTime.TranslateTo(9, 0, timeout), HeroImage3.TranslateTo(9, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(-5, 0, timeout), HeroImage1.TranslateTo(-5, 0, timeout), LabelDate.TranslateTo(-5, 0, timeout), LabelTime.TranslateTo(-5, 0, timeout), HeroImage3.TranslateTo(-5, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(5, 0, timeout), HeroImage1.TranslateTo(5, 0, timeout), LabelDate.TranslateTo(5, 0, timeout), LabelTime.TranslateTo(5, 0, timeout), HeroImage3.TranslateTo(5, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(-2, 0, timeout), HeroImage1.TranslateTo(-2, 0, timeout), LabelDate.TranslateTo(-2, 0, timeout), LabelTime.TranslateTo(-2, 0, timeout), HeroImage3.TranslateTo(-2, 0, timeout) });
            await Task.WhenAll(new List<Task> { BGSymbolImage.TranslateTo(2, 0, timeout), HeroImage1.TranslateTo(2, 0, timeout), LabelDate.TranslateTo(2, 0, timeout), LabelTime.TranslateTo(2, 0, timeout), HeroImage3.TranslateTo(2, 0, timeout) });
            BGSymbolImage.TranslationX = 0;
            LabelDate.TranslationX = 0;
            LabelTime.TranslationX = 0;
            HeroImage1.TranslationX = 0;
            HeroImage3.TranslationX = 0;
        }

        private Task<bool> BGColorChangeTask(Color from, Color to)
        {
            return BGSymbolImage.ColorTo(from, to, c => BGSymbolImage.BackgroundColor = c, 100);
        }
    }
}